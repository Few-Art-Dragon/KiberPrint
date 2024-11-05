using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public bool isGameOver { get; private set; }
    public bool isTrainingMode {get; private set; }
    public bool isEnglishAlphabet {get; private set; }

    public void Initialize()
    {
        isTrainingMode = false;
        isEnglishAlphabet = false;
        isGameOver = true;
    }

    public void SetGameOver(bool value)
    {
        isGameOver = value;
    }

    public void ToggleTrainingMode() => isTrainingMode = !isTrainingMode;
    public void ToggleAlphabet() => isEnglishAlphabet = !isEnglishAlphabet;
}
