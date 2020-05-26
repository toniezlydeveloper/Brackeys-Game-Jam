using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Button unpauseButton;
        [SerializeField] private CanvasGroup pauseScreenCanvasGroup;
        [SerializeField] private CanvasGroup loseScreenCanvasGroup;
        [SerializeField] private Player player;
        [SerializeField] private House house;

        public bool IsPaused { get; private set; }
        private bool _canTogglePause = true;
        private bool isGameOver;
        
        private void Awake()
        {
            player.OnPlayerDeath += GameOver;
            house.OnHouseDestroy += GameOver;
            unpauseButton.onClick.AddListener(Unpause);
        }

        private void Update()
        {
            if (_canTogglePause && Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(TogglePause(!IsPaused));
            }
        }

        private void GameOver()
        {
            isGameOver = true;
            StartCoroutine(StopTime());
            StartCoroutine(ShowLoseScreen());
        }

        private IEnumerator StopTime()
        {
            while (Time.timeScale > 0f)
            {
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, Time.time - Time.unscaledDeltaTime);
                yield return null;
            }
        }

        private IEnumerator ShowLoseScreen()
        {
            while (loseScreenCanvasGroup.alpha < 0.9995f)
            {
                loseScreenCanvasGroup.alpha += Time.unscaledDeltaTime;
                yield return null;
            }

            loseScreenCanvasGroup.alpha = 1f;
            loseScreenCanvasGroup.blocksRaycasts = true;
        }

        private void Unpause()
        {
            if (!_canTogglePause)
            {
                return;
            }
            
            StartCoroutine(TogglePause(false));
        }
        
        private IEnumerator TogglePause(bool state)
        {
            if (isGameOver)
            {
                yield break;
            }
            
            IsPaused = state;
            _canTogglePause = false;
            
            if (state)
            {
                Time.timeScale = 0f;
                while (pauseScreenCanvasGroup.alpha < 0.9995f)
                {
                    pauseScreenCanvasGroup.alpha += Time.unscaledDeltaTime * 2f;
                    yield return null;
                }

                pauseScreenCanvasGroup.alpha = 1f;
            }
            else
            {
                Time.timeScale = 1f;
                while (pauseScreenCanvasGroup.alpha > 0.0005f)
                {
                    pauseScreenCanvasGroup.alpha -= Time.unscaledDeltaTime * 2f;
                    yield return null;
                }

                pauseScreenCanvasGroup.alpha = 0f;
            }

            _canTogglePause = true;
        }
    }
}