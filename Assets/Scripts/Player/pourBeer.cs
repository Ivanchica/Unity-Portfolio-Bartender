using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class pourBeer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sfx_pourBeer;
    public AudioClip sfx_sendBeer;

    public GameObject mug;
    public GameObject mugFilled;
    private GameObject mugInHands;

    public float mugPosOffsetX, mugPosOffsetY;
    private bool b_mugIsFull;
    private MoveBartender bartender;

    // Start is called before the first frame update
    void Start()
    {
        b_mugIsFull = false;
        bartender = gameObject.GetComponent<MoveBartender>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            //if (Input.GetKeyDown(KeyCode.Mouse0) && bartender.b_CanPourBeer)
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                audioSource.PlayOneShot(sfx_pourBeer);

                // place the Bartender next to the tap
                bartender.MoveNearTap();
                // Fill the Mug, show it in Bartender's hand
                float newX = gameObject.transform.position.x + mugPosOffsetX;
                float newY = gameObject.transform.position.y - mugPosOffsetY;
                Vector3 mugPos = new Vector3(newX, newY, gameObject.transform.position.z);
                Quaternion mugRota = gameObject.transform.rotation;
                mugInHands = Instantiate(mugFilled, mugPos, mugRota);
                mugInHands.transform.parent = this.transform;
                b_mugIsFull = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (!b_mugIsFull)
                {
                    // Bartender is walking -> destroy the Mug
                    Destroy(mugInHands);
                }
                else
                {
                    // Mug is sent to the Customer
                    audioSource.PlayOneShot(sfx_sendBeer);

                    Destroy(mugInHands);
                    float newX = gameObject.transform.position.x + mugPosOffsetX;
                    float newY = gameObject.transform.position.y - mugPosOffsetY;
                    Vector3 mugPos = new Vector3(newX, newY, gameObject.transform.position.z);
                    Quaternion mugRota = gameObject.transform.rotation;
                    Instantiate(mug, mugPos, mugRota);
                }
            }
        }
    }

    public void AbortSendMug()
    {
        // player moved -> destroy the Mug
        b_mugIsFull = false;
        if (mugInHands != null)
        {
            Destroy(mugInHands);
        }
    }
}
