using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour
{
    public delegate void KeyPressedHandler();
    public static event KeyPressedHandler OnKeyPressed;

    private GameParameters _gameParameters;
    private LetterGenerator _letterGenerator;
    private ScoreBalance _scoreBalance;
    private ScoreDisplay _scoreDisplay;
    private GameTimer _gameTimer;
    private DifficultySettings _difficultySettings;

    private void Update()
    {
        CheckCorrectKeyDown();
    }

    public void Initialize(GameParameters gameParameters, LetterGenerator letterGenerator, ScoreBalance scoreBalance, ScoreDisplay scoreDisplay, GameTimer gameTimer, DifficultySettings difficultySettings)
    {
        _gameParameters = gameParameters;
        _letterGenerator = letterGenerator;
        _scoreBalance = scoreBalance;
        _scoreDisplay = scoreDisplay;
        _gameTimer = gameTimer;
        _difficultySettings = difficultySettings;
    }

    private void CheckCorrectKeyDown()
    {
        if (_gameParameters.isGameOver || Input.inputString.Length == 0) return;

        char inputChar = char.ToUpper(Input.inputString[0]);

        if (inputChar == _letterGenerator.currentLetter)
        {
            if (!_gameParameters.isTrainingMode) _scoreBalance.AddScore();
            OnKeyPressed?.Invoke();
            if (!_gameParameters.isTrainingMode)
            {
                _gameTimer.StartLetterTimer(_difficultySettings.GetLetterChangeTime(), _letterGenerator.GenerateNewLetter);
            }
        }
        else
        {
            if (!_gameParameters.isTrainingMode) _scoreBalance.SubtractScore();
        }
        _scoreDisplay.UpdateScore(_scoreBalance.Score);
    }
}
