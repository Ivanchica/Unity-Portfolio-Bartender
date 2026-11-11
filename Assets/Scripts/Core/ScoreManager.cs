using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CurrentScore { get; private set; }

    private void OnEnable()
    {
        ScoreEvents.OnPointsEarned -= AddPoints;
        ScoreEvents.OnPointsEarned += AddPoints;
        ScoreEvents.OnScoreChanged -= ChangeScore;
        ScoreEvents.OnScoreChanged += ChangeScore;
    }

    private void OnDisable()
    {
        ScoreEvents.OnPointsEarned -= AddPoints;
        ScoreEvents.OnScoreChanged -= ChangeScore;
    }

    void Start()
    {
        ResetScore();
    }

    private void AddPoints(int points)
    {
        CurrentScore += points;
        ScoreEvents.ChangeScore(CurrentScore);
    }

    private void ChangeScore(int newScore)
    {
        CurrentScore = newScore;
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        ScoreEvents.ChangeScore(CurrentScore);
    }
}
