using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject[] customers;

    public MoveBartender bartender;
    private CustomerSpawner customerSpawner;

    public bool win;

    private void Awake()
    {
        win = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(sfx_GameStart);
        customerSpawner = GetComponent<CustomerSpawner>();
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
            uiManager.UpdateLifes();
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
        customerSpawner.ResetCustomerSpawn();
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

        customers = GameObject.FindGameObjectsWithTag("Customer");
        foreach (GameObject customer in customers)
        {
            Destroy(customer);
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
        uiManager.UpdateLifes();
        uiManager.ShowGameOverUI();
        Invoke("DestroyMugsAndCustomers", 0.5f);
    }

    public void UpdateScoreUI()
    {
        uiManager.UpdateScore();
    }

    public void RestartGame()
    {
        // restart level, reset lives to 3, reset score, update UI
        win = false;
        DestroyMugsAndCustomers();
        bartender.resetBartenderPosition();
        customerSpawner.ResetCustomerSpawn();
        GameEnding.GameState = GameState.Playing;
        Lives.ResetLives();
        Score.score = 0;

        uiManager.RestartGame();

        // TODO
        // start the mini-game with shaking mugs
    }

    public void ContinuePlayingGame()
    {
        win = false;
        DestroyMugsAndCustomers();
        bartender.resetBartenderPosition();
        customerSpawner.ResetCustomerSpawn();
        GameEnding.GameState = GameState.Playing;

        uiManager.RestartGame();
    }

    public void MiniGameCans()
    {

    }
}
