using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMug : MonoBehaviour
{
    public float mugSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        if (GameEnding.GameState == GameState.Playing)
        {
            transform.Translate(-mugSpeed * Time.deltaTime, 0, 0);
        }
    }
}
