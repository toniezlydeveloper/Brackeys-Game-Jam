using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooting
{
    public class AmmoController : MonoBehaviour
    {
        [SerializeField] private int maxAmmoAmount;
        [SerializeField] private int ammoPerGroundPile;
        [SerializeField] private GroundPile groundPile;
        [SerializeField] private Image ammoMarker;
        [SerializeField] private TextMeshProUGUI text;

        private int _ammoAmount;

        private void Awake()
        {
            groundPile.OnGunReach += AddAmmo;
        }

        public bool HasEnoughAmmo()
        {
            return _ammoAmount > 0;
        }

        public void TakeAmmo()
        {
            _ammoAmount--;
            ammoMarker.fillAmount = (float) _ammoAmount / maxAmmoAmount;
            text.text = _ammoAmount.ToString();
        }
        
        private void AddAmmo()
        {
            _ammoAmount += ammoPerGroundPile;
            _ammoAmount = Mathf.Clamp(_ammoAmount, 0, maxAmmoAmount);

            ammoMarker.fillAmount = (float) _ammoAmount / maxAmmoAmount;
            text.text = _ammoAmount.ToString();
        }
    }
}