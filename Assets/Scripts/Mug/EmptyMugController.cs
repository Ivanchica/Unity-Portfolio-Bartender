using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMugController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sfx_EmptyMugPick;

    public GameObject explosion;
    public GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        audioSource = gameManager.audioSource;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            if (other.gameObject.tag == "EndOfBar")
            {
                //Krigla pala sa bara, jedan zivot manje
                GameObject explosionInstance = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(explosionInstance, 2f);
                Destroy(gameObject);
                gameManager.PlayerDied(); // zove f-ju koja oduzima jedan zivot, proverava da li je kraj igre
            }
            else if (other.gameObject.tag == "Player")
            {
                // BONUS igrac pokupio kriglu
                Destroy(gameObject);
                Score.AddScore(10);
                gameManager.UpdateScoreUI();
                audioSource.PlayOneShot(sfx_EmptyMugPick);
            }
        }

    }
}