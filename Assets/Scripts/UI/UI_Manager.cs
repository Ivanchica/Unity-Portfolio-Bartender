using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 


public class UI_Manager : MonoBehaviour
{
    public Text scoreNumber;
    public Image[] lifeIcons;
    public Canvas restartLevel;
    public TextMeshProUGUI restartText;
    public Canvas gameOverUI;
    public Canvas winUI;

    private float restartTimer;
    private bool b_showRestartWindow;

    private void OnEnable()
    {
        // unsubscribe first to avoid duplicate subscriptions if this object is enabled multiple times
        ScoreEvents.OnScoreChanged -= UpdateScoreText;
        ScoreEvents.OnScoreChanged += UpdateScoreText;

        Lives.OnLivesChanged -= HandleLivesChanged;
        Lives.OnLivesChanged += HandleLivesChanged;
    }
    private void OnDisable()
    {
        // Stop listening to prevent memory leaks if this object is destroyed
        ScoreEvents.OnScoreChanged -= UpdateScoreText;
        Lives.OnLivesChanged -= HandleLivesChanged;
    }

    private void Start()
    {
        RestartGame();
    }

    private void Update()
    {
        if (b_showRestartWindow)
        {
            restartTimer -= Time.deltaTime;
            if (restartTimer <= 0)
            {
                // hide Restart window
                b_showRestartWindow = false;
                restartLevel.enabled = false;
            }
            else
            {
                int timer = (int)(1+ restartTimer);
                restartText.text = "Ready to serve in: " + timer + "s";
            }
        }
    }

    private void UpdateScoreText(int newScore)
    {       
        scoreNumber.text = "" + newScore; // Update the score display
    }

    public void UpdateLives()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i > Lives.livesLeft)
            {
                lifeIcons[i].enabled = false;
            }
        }
    }

    private void HandleLivesChanged(int newLives)
    {
        UpdateLives();
    }

    public void displayRestartLevel()
    {
        restartLevel.enabled = true;
        restartTimer = 5f;
        b_showRestartWindow = true;
    }

    public void RestartGame()
    {
        foreach (Image image in lifeIcons)
        {
            image.enabled = true;
        }

        restartLevel.enabled = false;
        restartTimer = 0f;
        b_showRestartWindow = false;

        winUI.enabled = false;
        gameOverUI.enabled = false;

        UpdateLives();
    }

    public void ContinuePlayingGame()
    {
        restartLevel.enabled = false;
        restartTimer = 0f;
        b_showRestartWindow = false;

        winUI.enabled = false;
        gameOverUI.enabled = false;

        UpdateLives();
    }

    public void ShowGameWinUI()
    {
        winUI.enabled = true;
    }

    public void ShowGameOverUI()
    {
        gameOverUI.enabled = true;
    }
}
