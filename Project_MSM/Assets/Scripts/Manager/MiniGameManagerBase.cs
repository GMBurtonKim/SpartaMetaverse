using UnityEngine;

public abstract class MiniGameManagerBase : MonoBehaviour
{
    public static MiniGameManagerBase Instance { get; protected set; }

    protected int currentScore = 0;
    protected int bestScore = 0;

    public bool IsGameStart { get; protected set; } = false;

    public virtual void StartGame()
    {
        currentScore = 0;
        IsGameStart = true;
    }

    public virtual void AddScore(int score)
    {
        if (!IsGameStart) return;
        currentScore += score;
        if (currentScore > bestScore)
            bestScore = currentScore;
    }

    public virtual void GameOver() 
    {
        IsGameStart = false;

    }
    public virtual void Restart() 
    {
        StartGame();
    }

    public int GetScore() => currentScore;
    public int GetBestScore() => bestScore;
}