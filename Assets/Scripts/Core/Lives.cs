using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public static int livesLeft;
    public static event System.Action<int> OnLivesChanged;

    private void Start()
    {
        ResetLives();
    }

    public static void ResetLives()
    {
        livesLeft = 2;
        OnLivesChanged?.Invoke(livesLeft);
    }

    public static void DecreaseLives()
    {
        livesLeft--;
        OnLivesChanged?.Invoke(livesLeft);
    }
}
