using UnityEngine;

public class ScoreBalance: MonoBehaviour
{
    public ushort Score { get; private set; }

    public void Initialize()
    {
        Score = 0;
    }

    public void AddScore()
    {
        Score++;
    }

    public void SubtractScore()
    {
        if (Score != 0)
        {
            Score--;
        }
    }
}
