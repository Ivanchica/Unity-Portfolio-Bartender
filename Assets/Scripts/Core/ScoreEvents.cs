using System;

public static class ScoreEvents
{
    public static event System.Action<int> OnScoreChanged;
    public static event System.Action<int> OnPointsEarned;

    public static void AddPoints(int points)
    {
        OnPointsEarned?.Invoke(points);
    }

    public static void ChangeScore(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
}