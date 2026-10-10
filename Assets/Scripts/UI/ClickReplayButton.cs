using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickReplayButton : MonoBehaviour
{
    public AudioSource audioSound;
    public GameManagerScript gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    public void onClick()
    {
        if (audioSound)
        {
            audioSound.Play();
            gameManager.RestartGame();
        }
    }
}
