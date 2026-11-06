using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sfx_walk;

    public Transform[] beginningOfBar;

    public int bartenderPosition = 0;
    public Transform[] SpawnPosition;
    public float bartenderSpeed;

    private pourBeer pourBeerComponent;
    private BartenderControls inputActions;
    private Vector2 movementInput;
    private float previousLaneInput;
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

    private void OnEnable()
    {
        inputActions = new BartenderControls();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        if (inputActions == null) { return; }

        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Dispose();
        inputActions = null;
        movementInput = Vector2.zero;
        previousLaneInput = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            if (movementInput.x < -0.5f)
            {
                pourBeerComponent.AbortSendMug();
                MoveLeft();
            }

            if (movementInput.x > 0.5f)
            {
                pourBeerComponent.AbortSendMug();
                MoveRight();
            }
        }

        distanceToTap = SpawnPosition[bartenderPosition].position.x - transform.position.x;
        if (distanceToTap < pourBeerOffset) { b_CanPourBeer = true; }
        else { b_CanPourBeer = false; }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        if (GameEnding.GameState == GameState.Playing)
        {
            if (movementInput.y < -0.5f && previousLaneInput >= 0f)
            {
                if (pourBeerComponent != null) { pourBeerComponent.AbortSendMug(); }
                MoveDown();
            }
            else if (movementInput.y > 0.5f && previousLaneInput <= 0f)
            {
                if (pourBeerComponent != null) { pourBeerComponent.AbortSendMug(); }
                MoveUp();
            }
        }

        previousLaneInput = movementInput.y;
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
        if (bartenderPosition >= SpawnPosition.Length) { bartenderPosition = 0; }
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
        if (bartenderPosition < 0) { bartenderPosition = SpawnPosition.Length - 1; }
        gameObject.transform.position = new Vector3(
            SpawnPosition[bartenderPosition].position.x,
            SpawnPosition[bartenderPosition].position.y,
            gameObject.transform.position.z);
        b_CanPourBeer = true;
    }

    void MoveLeft()
    {
        float leftBoundary = beginningOfBar[bartenderPosition].position.x;
        Vector3 position = transform.position;
        position.x = Mathf.Max(leftBoundary, position.x - bartenderSpeed * Time.deltaTime);
        transform.position = position;
    }
    
    void MoveRight()
    {
        float rightBoundary = SpawnPosition[bartenderPosition].position.x;
        Vector3 position = transform.position;
        position.x = Mathf.Min(rightBoundary, position.x + bartenderSpeed * Time.deltaTime);
        transform.position = position;
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

