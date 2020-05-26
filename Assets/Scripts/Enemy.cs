using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float moveSpeed;
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Hole"))
        {
            ScoreController.Instance.AddScore(10);
            Die();
        }
        else if (other.transform.CompareTag("GroundBullet"))
        {
            ScoreController.Instance.AddScore(10);
            Die();
        }
        else if (other.transform.CompareTag("House"))
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
