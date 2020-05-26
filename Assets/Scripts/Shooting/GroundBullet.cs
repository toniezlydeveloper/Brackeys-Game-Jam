using System;
using UnityEngine;

namespace Shooting
{
    public class GroundBullet : MonoBehaviour
    {
        [SerializeField] private GameObject destroyParticles;
        [SerializeField] private  Rigidbody2D rb;
        [SerializeField] private float shootPower;
        
        private bool _canMove;

        private void Awake()
        {
            Destroy(gameObject, 2.5f);
        }

        public void LaunchInDirection(Vector2 direction)
        {
            rb.AddForce(shootPower * direction);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Instantiate(destroyParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}