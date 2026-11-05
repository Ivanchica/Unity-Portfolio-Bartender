using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveBartender : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sfx_walk;

    public Transform[] beginningOfBar;

    public int bartenderPosition = 0;
    public Transform[] SpawnPosition;
    public float bartenderSpeed;

    private pourBeer pourBeerComponent;
    public bool b_CanPourBeer;
    public float pourBeerOffset;
    private float distanceToTap;

    // Start is called before the first frame update
    void Start()
    {
        pourBeerComponent = this.gameObject.GetComponent<pourBeer>();

        resetBartenderPosition();
        bartenderSpeed = 2f;
        b_CanPourBeer = true;
        pourBeerOffset = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.S)) 
            {
                pourBeerComponent.AbortSendMug();
                MoveDown(); 
            }
            if (Input.GetKeyDown(KeyCode.W)) 
            {
                pourBeerComponent.AbortSendMug();
                MoveUp();
            }

            if (Input.GetKey(KeyCode.A))
            {
                pourBeerComponent.AbortSendMug();
                MoveLeft();
            }

            if (Input.GetKey(KeyCode.D))
            {
                pourBeerComponent.AbortSendMug();
                MoveRight();
            }
        }

        distanceToTap = SpawnPosition[bartenderPosition].position.x - transform.position.x;
        if (distanceToTap < pourBeerOffset) { b_CanPourBeer = true; }
        else { b_CanPourBeer = false; }
    }

    public void resetBartenderPosition()
    {
        // Place the Bartender at a start position at the beginning
        gameObject.transform.position = new Vector3(
            SpawnPosition[0].position.x,
            SpawnPosition[0].position.y,
            gameObject.transform.position.z);
        bartenderPosition = 0;
    }

    void MoveDown()
    {
        audioSource.PlayOneShot(sfx_walk);
        bartenderPosition++;
        if (bartenderPosition > 3) { bartenderPosition = 0; }
        gameObject.transform.position = new Vector3(
            SpawnPosition[bartenderPosition].position.x,
            SpawnPosition[bartenderPosition].position.y,
            gameObject.transform.position.z);
        b_CanPourBeer = true;
    }

    void MoveUp()
    {
        audioSource.PlayOneShot(sfx_walk);
        bartenderPosition--;
        if (bartenderPosition < 0) { bartenderPosition = 3; }
        gameObject.transform.position = new Vector3(
            SpawnPosition[bartenderPosition].position.x,
            SpawnPosition[bartenderPosition].position.y,
            gameObject.transform.position.z);
        b_CanPourBeer = true;
    }

    void MoveLeft()
    {
        if (transform.position.x > beginningOfBar[bartenderPosition].position.x)
        {
            transform.position += new Vector3(-bartenderSpeed * Time.deltaTime, 0f, 0f);
        }
    }
    
    void MoveRight()
    {
        if (transform.position.x < SpawnPosition[bartenderPosition].position.x)
        {
            transform.position += new Vector3(bartenderSpeed * Time.deltaTime, 0f, 0f);
        }
    }

    public void MoveNearTap()
    {
        pourBeerComponent.AbortSendMug();
        gameObject.transform.position = new Vector3(
            SpawnPosition[bartenderPosition].position.x,
            SpawnPosition[bartenderPosition].position.y,
            gameObject.transform.position.z);
        b_CanPourBeer = true;
    }
}

