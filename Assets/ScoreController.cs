using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    
    private int _score;

    private static ScoreController instance;

    public static ScoreController Instance => instance;
    
    private void Awake()
    {
        instance = this;
        UpdateScoreTexts();
    }

    public void AddScore(int scoreAmount)
    {
        _score += scoreAmount;
        UpdateScoreTexts();
    }

    private void UpdateScoreTexts()
    {
        text1.text = $"Score: {_score}";
        text2.text = $"Score: {_score}";
    }
}
