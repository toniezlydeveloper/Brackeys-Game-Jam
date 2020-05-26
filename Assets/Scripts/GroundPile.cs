using System;
using UnityEngine;

public class GroundPile : MonoBehaviour
{
    public event Action OnGunReach;
        
    [SerializeField] private Transform gunTransform;
    [SerializeField] private float moveTime;

    private float _distance;
    private bool _canMove;

    private void Awake()
    {
        OnGunReach += () => gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!_canMove)
        {
            return;
        }

        Vector3 difference = gunTransform.position - transform.position;
        Vector3 translation = difference.normalized / moveTime;
            
        transform.Translate(translation * Time.deltaTime);
        transform.position += new Vector3(0f, Mathf.Sin(difference.magnitude / _distance) * Time.deltaTime * 3.5f);

        if (difference.magnitude <= 0.25f)
        {
            OnGunReach?.Invoke();
        }
    }

    public void MoveToGun(Vector3 initialPosition)
    {
        _distance = (initialPosition - gunTransform.position).magnitude;
        
        transform.position = initialPosition;
        gameObject.SetActive(true);
        _canMove = true;
    }
}