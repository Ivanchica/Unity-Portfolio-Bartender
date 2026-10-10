using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

// Controls the Customer movement
public class Customer : MonoBehaviour
{
    public GameManagerScript gameManager;

    public bool drinkingBeer;
    public bool endOfBar = false;
    private bool moveBack = false;

    public float customerSpeed;
    public float goBackSpeed;
    public float goBackDistance;
    private float stopDistance;
    private float startDrinkingTime;
    public float endDrinkingTime;

    public GameObject mugSprite;
    public GameObject explosion;
    public GameObject emptyMug;

    // Start is called before the first frame update
    void Start()
    {
        endOfBar = false;
        drinkingBeer = false;
        moveBack = false;

        goBackDistance = 3f;
        goBackSpeed = 4f;
        customerSpeed = 1f;
        endDrinkingTime = 5f;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            if (drinkingBeer)
            {

                if (stopDistance > gameObject.transform.position.x)
                {
                    moveBack = false;
                }
                if (moveBack)
                {
                    transform.Translate(-goBackSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    // if the timer for drinking ran out -> move the customer
                    float deltaTimer = Time.time - startDrinkingTime;
                    if (deltaTimer > endDrinkingTime)
                    {
                        drinkingBeer = false;
                        mugSprite.GetComponent<SpriteRenderer>().enabled = false;
                        Instantiate(emptyMug, gameObject.transform.position, gameObject.transform.rotation);
                    }
                }
            }
            else if (endOfBar == false)
            {
                transform.Translate(customerSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            if (other.gameObject.tag == "EndOfBar")
            {
                // Customer reached the end of the bar -> game over
                GameObject explosionInstance = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(explosionInstance, 2f);
                endOfBar = true;
                gameManager.PlayerDied(); // decrease one life and check the end game condition
            }
            if (other.gameObject.tag == "BeginningOfBar")
            {
                Destroy(gameObject);
            }
        }
    }

    public void drinkBeer ()
    {
        drinkingBeer = true;
        moveBack = true;
        stopDistance = gameObject.transform.position.x - goBackDistance;
        mugSprite.GetComponent<SpriteRenderer>().enabled = true;
        startDrinkingTime = Time.time;
    }

    private void OnEnable()
    {
        CustomerSpawner.customerCount++;
    }

    private void OnDisable()
    {
        CustomerSpawner.customerCount--;
    }
}

