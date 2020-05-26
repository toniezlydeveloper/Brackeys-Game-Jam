using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button gameOverButton;
    [SerializeField] private Button gameOverButton2;
    [SerializeField] private Button playTimeButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button backToMenuButtonCredits;
    

    private void Awake()
    {
        Time.timeScale = 0f;
        helpButton.onClick.AddListener(() => helpPanel.SetActive(true));
        backToMenuButton.onClick.AddListener(() => helpPanel.SetActive(false));
        creditsButton.onClick.AddListener(() => creditsPanel.SetActive(true));
        backToMenuButtonCredits.onClick.AddListener(() => creditsPanel.SetActive(false));
        quitGameButton.onClick.AddListener(Application.Quit);
        playTimeButton.onClick.AddListener(() => StartCoroutine(StartGame()));
        gameOverButton.onClick.AddListener(ReloadScene);
        gameOverButton2.onClick.AddListener(ReloadScene);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator StartGame()
    {
        canvasGroup.blocksRaycasts = false;
        while (canvasGroup.alpha > 0.0005f)
        {
            canvasGroup.alpha -= Time.unscaledDeltaTime * 2f;
            yield return null;
        }

        Time.timeScale = 1;
        canvasGroup.alpha = 0;
    }
}
