using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public static int livesLeft;

    private void Start()
    {
        ResetLives();
    }

    public static void ResetLives()
    {
        livesLeft = 2;
    }

    public static void DecreaseLives()
    {
        livesLeft--;
    }
}
