using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    public static bool isCheatOn;
    public Text cheatOn;

    private void Start()
    {
        isCheatOn = false;
        cheatOn.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isCheatOn) 
            { 
                isCheatOn = false;
                cheatOn.enabled = false;
            }
            else 
            { 
                isCheatOn = true;
                cheatOn.enabled = true;
            }
        }
    }
}
