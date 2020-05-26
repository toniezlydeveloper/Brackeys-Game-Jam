using System;
using DefaultNamespace;
using Digging;
using UnityEngine;

namespace Shooting
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform shootingPoint;
        [SerializeField] private GameObject groundBulletPrefab;
        [SerializeField] private AmmoController ammoController;
        [SerializeField] private Player player;
        [SerializeField] private SpriteRenderer rendererWithPlayer;
        [SerializeField] private SpriteRenderer rendererWithoutPlayer;
        [SerializeField] private GameController controller;

        private void Awake()
        {
            player.OnGunStateChange += ChangeSprite;
        }

        private void Update()
        {
            HandleShooting();
        }

        private void ChangeSprite(bool state)
        {
            if (state)
            {
                rendererWithoutPlayer.enabled = true;
                rendererWithPlayer.enabled = false;    
            }
            else
            {
                rendererWithoutPlayer.enabled = false;
                rendererWithPlayer.enabled = true;
            }
        }

        private void HandleShooting()
        {
            if (controller.IsPaused)
            {
                return;
            }
            
            if (!player.IsOnGun)
            {
                return;
            }

            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            if (!ammoController.HasEnoughAmmo())
            {
                return;
            }
            
            GroundBullet groundBullet = Instantiate(groundBulletPrefab, shootingPoint).GetComponent<GroundBullet>();
            
            if (groundBullet == null)
            {
                return;
            }

            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 gunPosition = shootingPoint.position;
            
            groundBullet.LaunchInDirection((mousePosition - gunPosition).normalized);
            ammoController.TakeAmmo();
        }
    }
}