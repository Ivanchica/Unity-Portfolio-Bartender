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
                // sakriva restart prozor
                b_showRestartWindow = false;
                restartLevel.enabled = false;
            }
            else
            {
                int timer = (int)(1+ restartTimer);
                restartText.text = "Ready to server in: " + timer + "s";
            }
        }
    }

    public void UpdateScore()
    {
        scoreNumber.text = "" + Score.score;
    }

    public void UpdateLifes()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i > Lives.livesLeft)
            {
                lifeIcons[i].enabled = false;
            }
        }
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

        UpdateScore();
        UpdateLifes();
    }

    public void ContinuePlayingGame()
    {
        restartLevel.enabled = false;
        restartTimer = 0f;
        b_showRestartWindow = false;

        winUI.enabled = false;
        gameOverUI.enabled = false;

        UpdateScore();
        UpdateLifes();
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
