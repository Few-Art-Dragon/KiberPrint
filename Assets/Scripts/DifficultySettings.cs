using UnityEngine;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

public class DifficultySettings : MonoBehaviour
{
    public DifficultyLevel difficultyLevel { get; private set; }
    public void Initialize()
    {
        difficultyLevel = DifficultyLevel.Easy;
    }

    public void ToggleDifficulty()
    {
        difficultyLevel = difficultyLevel switch
        {
            DifficultyLevel.Easy => DifficultyLevel.Medium,
            DifficultyLevel.Medium => DifficultyLevel.Hard,
            DifficultyLevel.Hard => DifficultyLevel.Easy,
            _ => difficultyLevel
        };
    }

    public float GetLetterChangeTime()
    {
        return difficultyLevel switch
        {
            DifficultyLevel.Easy => 5f,
            DifficultyLevel.Medium => 3f,
            DifficultyLevel.Hard => 2f,
            _ => 3f
        };
    }
}
