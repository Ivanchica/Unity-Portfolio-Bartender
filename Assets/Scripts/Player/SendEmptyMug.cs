using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEmptyMug : MonoBehaviour
{
    public float mugSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        mugSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            transform.Translate(mugSpeed * Time.deltaTime, 0, 0);
        }
    }
}
