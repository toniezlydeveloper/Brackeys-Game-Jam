using UnityEngine;

namespace Digging
{
    public class HoleFiller : MonoBehaviour
    {
        [SerializeField] private GroundTile groundTile;

        private void OnCollisionEnter2D(Collision2D other)
        {
            groundTile.FillHole();
        }
    }
}
