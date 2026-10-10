using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : MonoBehaviour
{
    public GameObject explosion;
    public GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            if (other.gameObject.tag == "Customer" && other.GetComponent<Customer>().drinkingBeer == false)
            {
                //Krigla dosla do mushterije koja cheka pivo
                Destroy(gameObject);
                other.GetComponent<Customer>().drinkBeer();
                Score.AddScore(100);
                gameManager.UpdateScoreUI();
            }
            else if (other.gameObject.tag == "BeginningOfBar")
            {
                // krigla udarila u pocetak bara - jedan zivot manje
                GameObject explosionInstance = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(explosionInstance, 2f);
                Destroy(gameObject);
                gameManager.PlayerDied(); // zove f-ju koja oduzima jedan zivot, proverava da li je kraj igre
            }
        }
        
    }
}
