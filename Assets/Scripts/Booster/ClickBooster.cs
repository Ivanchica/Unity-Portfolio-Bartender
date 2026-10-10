using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBooster : MonoBehaviour
{
    public AudioSource audioSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onClick()
    {
        if (audioSound)
        {
            audioSound.Play();
        }
    }
}
