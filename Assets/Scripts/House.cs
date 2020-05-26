using System;
using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    public event Action OnHouseDestroy;
    [SerializeField] private CanvasGroup heart1;
    [SerializeField] private CanvasGroup heart2;
    [SerializeField] private CanvasGroup heart3;
    
    [SerializeField] private int maxLives;

    private int _currentLives;

    private void Awake()
    {
        _currentLives = maxLives;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Enemy"))
        {
            return;
        }
        
        if (--_currentLives < 1)
        {
            OnHouseDestroy?.Invoke();
        }

        CanvasGroup heartCanvasGroup;
        
        switch (_currentLives)
        {
            case 0:
                heartCanvasGroup = heart1;
                break;
            case 1: 
                heartCanvasGroup = heart2;
                break;
            case 2: 
                heartCanvasGroup = heart3;
                break;
            default:
                heartCanvasGroup = heart1;
                break;
        }
        
        heartCanvasGroup.alpha = 0f;
    }
}
