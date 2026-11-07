using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public static event System.Action<int> OnScoreChanged;

    public static void AddScore(int deltaScore)
    {
        score += deltaScore;
        OnScoreChanged?.Invoke(score);
    }
}
