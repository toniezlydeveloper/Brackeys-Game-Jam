using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float peakThreshold;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float peakSpeed;
    [SerializeField] private Transform target;

    private bool IsPeaking => transform.position.x < peakThreshold;
    
    private void Update()
    {
        Move();
    }

    public void AssignTarget(Transform targetToAssign)
    {
        target = targetToAssign;
    }

    private void Move()
    {
        Vector2 moveVector = IsPeaking ? (Vector2) (target.position - transform.position).normalized : Vector2.left;
        float speed = IsPeaking ? peakSpeed : moveSpeed;
        transform.Translate(speed * Time.deltaTime * moveVector);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("House"))
        {
            Die();
        }
        else if (other.transform.CompareTag("GroundBullet"))
        {
            ScoreController.Instance.AddScore(15);
            Die();
        }
        
    }

    private void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
