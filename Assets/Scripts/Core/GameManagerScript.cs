using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManagerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sfx_GameOver;
    //public AudioClip sfx_GameRestart;
    public AudioClip sfx_GameWin;
    public AudioClip sfx_Crash;
    public AudioClip sfx_GameStart;

    public UI_Manager uiManager;
    public GameEnding gameEnding;

    private GameObject[] mugs;
    private GameObject[] emptyMugs;

    [SerializeField] private PlayerController bartender;
    [SerializeField] private CustomerSpawner customerSpawner;

    public bool win;

    private void Awake()
    {
        win = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(sfx_GameStart);
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (win == false)
        {
            if (GameEnding.GameState == GameState.Playing &&  CustomerSpawner.customerCount == 0)
            {
                PlayerWin();
            }
        }
    }

    public void PlayerDied()
    {
        audioSource.PlayOneShot(sfx_Crash);

        Lives.DecreaseLives();
        if (Lives.livesLeft < 0)
        {
            // game over
            GameOver();
            audioSource.PlayOneShot(sfx_GameOver);

        }
        else
        {
            // restart level
            uiManager.UpdateLives();
            gameEnding.GameRestartLevel();
            uiManager.displayRestartLevel();
            Invoke("DestroyMugsAndCustomers", 0.1f);
            Invoke("RestartLevel", 5f);
            //audioSource.PlayOneShot(sfx_GameRestart);
        }
    }

    void RestartLevel()
    {
        bartender.resetBartenderPosition();
        ResetCustomerPosition();
        GameEnding.GameState = GameState.Playing;
    }

    void DestroyMugsAndCustomers()
    {
        emptyMugs = GameObject.FindGameObjectsWithTag("EmptyMug");
        foreach (GameObject mug in emptyMugs)
        {
            Destroy(mug);
        }

        mugs = GameObject.FindGameObjectsWithTag("Mug");
        foreach (GameObject mug in mugs)
        {
            Destroy(mug);
        }

        // Destroy all active customers
        if (customerSpawner != null)
        {
            customerSpawner.ClearActiveCustomers();
        }
        else
        {
            Debug.LogError("CustomerSpawner reference is missing in GameManagerScript.");
        }
    }

    public void PlayerWin()
    {
        audioSource.PlayOneShot(sfx_GameWin);
        win = true;
        gameEnding.GameWin();
        uiManager.ShowGameWinUI();
        Invoke("DestroyMugsAndCustomers", 0.5f);
    }

    public void GameOver()
    {
        gameEnding.GameOver();
        uiManager.UpdateLives();
        uiManager.ShowGameOverUI();
        Invoke("DestroyMugsAndCustomers", 0.5f);
    }

    public void RestartGame()
    {
        // restart level, reset lives to 3, reset score, update UI
        win = false;
        DestroyMugsAndCustomers();
        bartender.resetBartenderPosition();
        ResetCustomerPosition();
        GameEnding.GameState = GameState.Playing;
        Lives.ResetLives();
        ScoreEvents.ChangeScore(0);

        uiManager.RestartGame();

        // TODO
        // start the mini-game with shaking mugs
    }

    public void ContinuePlayingGame()
    {
        win = false;
        DestroyMugsAndCustomers();
        bartender.resetBartenderPosition();
        ResetCustomerPosition();
        GameEnding.GameState = GameState.Playing;

        uiManager.RestartGame();
    }

    public void ResetCustomerPosition()
    {
        if (customerSpawner != null)
        {
            customerSpawner.ResetCustomerSpawn();
        }
        else
        {
            Debug.LogError("CustomerSpawner reference is missing in GameManagerScript.");
        }
    }

    public void MiniGameCans()
    {

    }

    public void QuitGame()
    {
        // Confirms the button works while testing in Unity
        Debug.Log("Game is exiting..."); 
        
        // Closes the actual built application
        Application.Quit(); 
    }
}
