using System;
using UnityEngine;
using UnityEngine.UI;

namespace Digging
{
    public class DiggingController : MonoBehaviour
    {
        [SerializeField] private Image diggingMarker;
        [SerializeField] private Image diggingStacksMarker;
        [SerializeField] private float diggingDistanceThreshold;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Player player;
        [SerializeField] private float diggingInterval;
        [SerializeField] private int maxDiggingStacks = 3;

        private float _diggingTime;
        private int _diggingStacks;

        private void Awake()
        {
            _diggingStacks = maxDiggingStacks;
        }

        private void Update()
        {
            UpdateDiggingMarker();
            HandleInput();
        }

        private bool AddDiggingStack()
        {
            if (_diggingStacks == maxDiggingStacks)
            {
                return false;
            }
            _diggingStacks++;
            return true;
        }

        private void HandleInput()
        {
            
            if (Time.time < _diggingTime)
            {
                return;
            }
            
            if (player.IsOnGun)
            {
                return;
            }
            
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
        
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePositionFixed = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePositionFixed, Vector2.zero);

            if (hit.transform == null)
            {
                return;
            }

            if (!IsGroundTileInRange(hit.transform.position))
            {
                return;
            }
        
            GroundTile groundTile = hit.transform.GetComponentInParent<GroundTile>();
        
            if (groundTile == null)
            {
                return;
            }
            
            if (_diggingStacks < 1 && !groundTile.IsFilled)
            {
                return; 
            }

            _diggingTime = Time.time + diggingInterval;

            if (groundTile.IsFilled)
            {
                if (AddDiggingStack())
                    groundTile.CollectBones(true);
            }
            else
            {
                groundTile.CreateHole();
                _diggingStacks--;
            }

            diggingStacksMarker.fillAmount = (float) _diggingStacks / maxDiggingStacks;
        }

        private void UpdateDiggingMarker()
        {
            if (diggingMarker == null)
            {
                return;
            }
            
            diggingMarker.fillAmount = 1- ((_diggingTime - Time.time) / diggingInterval);
        }

        private bool IsGroundTileInRange(Vector2 groundTilePosition)
        {
            if (playerTransform == null)
            {
                return false;
            }
            
            return Vector2.Distance(groundTilePosition, playerTransform.position) <= diggingDistanceThreshold;
        }
    }
}
