using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public enum GameState { Paused, Playing, GameOver, Win, RestartLevel }

public class GameEnding : MonoBehaviour
{
    public static GameState GameState { get; set; }

    public GameObject gameOverText;
    public GameObject gameWinText;

    // Start is called before the first frame update
    void Start()
    {
        GameState = GameState.Playing;
    }

    private void Awake()
    {
        GameState = GameState.Playing;
    }

    public void GameOver()
    {
        // game over, game lose
        if (!Cheat.isCheatOn)
        {
            GameState = GameState.GameOver;
        }
    }

    public void GameWin()
    {
        // game win
        GameState = GameState.Win;
    }

    public void GameRestartLevel()
    {
        // restart level
        GameState = GameState.RestartLevel;

    }
}
