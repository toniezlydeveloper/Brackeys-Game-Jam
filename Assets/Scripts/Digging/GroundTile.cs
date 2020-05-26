using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Digging
{
    public class GroundTile : MonoBehaviour
    {
        [SerializeField] private float renewInterval = 3f;
        [SerializeField] private GroundPile groundPile;
        [SerializeField] private GameObject groundTop;
        [SerializeField] private GameObject filledHole;
        [SerializeField] private Image renewMarker;
        [SerializeField] private GameObject renewMarkerObject;

        public bool IsFilled { get; private set; }
        private float _renewTime;

        private void Update()
        {
            if (!IsFilled)
            {
                return;
            }

            float difference = _renewTime - Time.time;
            
            if (difference > 0f)
            {
                renewMarker.fillAmount = 1 - (difference / renewInterval);
                return;
            }
            
            CollectBones(false);
        }

        public void CreateHole()
        {
            if (IsFilled)
            {
                return;
            }
            
            Vector3 pilePosition = transform.position + new Vector3(0f, .75f, 0f);
            
            groundPile.MoveToGun(pilePosition);
            groundTop.SetActive(false);
        }

        public void CollectBones(bool didPlayerTake)
        {
            if (!IsFilled)
            {
                return;
            }

            if (didPlayerTake)
            {
                ScoreController.Instance.AddScore(5);
            }

            IsFilled = false;
            groundTop.SetActive(true);
            filledHole.SetActive(false);
            renewMarkerObject.SetActive(false);
        }

        public void FillHole()
        {
            renewMarkerObject.SetActive(true);
            _renewTime = Time.time + renewInterval;
            filledHole.SetActive(true);
            IsFilled = true;
        }
    }
}
