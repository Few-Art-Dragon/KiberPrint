using UnityEngine;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard,
    Extreme
}

public class DifficultySettings : MonoBehaviour
{
    public DifficultyLevel difficultyLevel { get; private set; }
    private DifficultyLevel _prevDifficultyLevel;
    public void Initialize()
    {
        difficultyLevel = DifficultyLevel.Easy;
    }

    public void SetDifficultyLevel()
    {
        if (difficultyLevel != DifficultyLevel.Extreme)
        {
            _prevDifficultyLevel = difficultyLevel;
            difficultyLevel = DifficultyLevel.Extreme;
        }
        else if (difficultyLevel == DifficultyLevel.Extreme) 
        {
            difficultyLevel = _prevDifficultyLevel;
        }
            
    }

    public void ToggleDifficulty()
    {
        difficultyLevel = difficultyLevel switch
        {
            DifficultyLevel.Easy => DifficultyLevel.Medium,
            DifficultyLevel.Medium => DifficultyLevel.Hard,
            DifficultyLevel.Hard => DifficultyLevel.Easy,
            DifficultyLevel.Extreme => DifficultyLevel.Easy,
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
            DifficultyLevel.Extreme => 0.95f,
            _ => 3f
        };
    }
}
