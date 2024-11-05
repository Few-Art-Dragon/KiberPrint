using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrapper : MonoBehaviour
{
    private enum GameState
    {
        Playing,
        MainMenu
    }

    [SerializeField] private LetterGenerator _letterGenerator;    
    [SerializeField] private LetterDisplay _letterDisplay;
    [SerializeField] private ScoreDisplay _scoreDisplay;
    [SerializeField] private ScoreBalance _scoreBalance;
    [SerializeField] private KeyboardInputHandler _keyboardInputHandler;
    [SerializeField] private GameParameters _gameParameters;
    [SerializeField] private DifficultySettings _difficultySettings;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private TimerViewMainMenu _timerViewMainMenu;
    [SerializeField] private GameTimer _gameTimer;
    [SerializeField] private HoldEscapeHandler _holdEscapeHandler;

    private GameState _currentState;

    private void Awake()
    {
        InitializeMainMenu();
        _currentState = GameState.MainMenu;
    }

    private void InitializeMainMenu()
    {
        _gameUI.ShowMainMenu();
        _holdEscapeHandler.Subscribe(ResolveExitPath);
        _gameParameters.Initialize();
        _difficultySettings.Initialize();
        _scoreBalance.Initialize();
        _keyboardInputHandler.Initialize(_gameParameters, _letterGenerator, _scoreBalance, _scoreDisplay, _gameTimer, _difficultySettings);
        _gameTimer.Initialize();
        _timerViewMainMenu.Initialize(_gameTimer);
    }

    public void StartGame()
    {
        _gameUI.HideMainMenu();
        _currentState = GameState.Playing;

        InitializeGameComponents();

        if (!_gameParameters.isTrainingMode)
        {
            _letterGenerator.OnLetterGenerated += RestartTimer;
            _gameTimer.StartMatchTimer(_gameTimer.MatchTimeInSeconds, EndGame);
        }
    }
    private void InitializeGameComponents()
    {
        _letterDisplay.Initialize(_letterGenerator);
        _letterDisplay.UpdateText();
        _letterGenerator.Initialize(_gameParameters);
        _scoreDisplay.Initialize(_gameParameters);
        _gameParameters.SetGameOver(false);
    }

    private void RestartTimer()
    {
        _gameTimer.StartLetterTimer(_difficultySettings.GetLetterChangeTime(), _letterGenerator.GenerateNewLetter);
    }

    public void EndGame()
    {
        FinalizeGameState();
        _gameUI.ShowResultMenu();
        _letterDisplay.UpdateText(_scoreBalance.Score);
    }

    public void RestartGame()
    {
        _scoreDisplay.ResetText();
        _scoreBalance.Initialize();
        _gameTimer.StopAllTimers();
        _gameUI.HideResultMenu();

        StartNewMatch();
    }

    private void StartNewMatch()
    {
        _gameTimer.StartMatchTimer(_gameTimer.MatchTimeInSeconds, EndGame);
        _gameParameters.SetGameOver(false);
        _letterGenerator.GenerateNewLetter();
    }

    private void FinalizeGameState()
    {
        _gameTimer.StopAllTimers();
        _gameParameters.SetGameOver(true);
    }

    private void ResolveExitPath(InputAction.CallbackContext context)
    {
        if (_currentState == GameState.Playing)
        {
            ExitToMainMenu();
        }
        else
        {
            ExitGame();
        }
    }

    private void ExitToMainMenu()
    {
        FinalizeGameState();
        _currentState = GameState.MainMenu;

        _letterDisplay.UpdateText("");
        _scoreDisplay.ResetText();
        _scoreBalance.Initialize();

        _gameUI.HideResultMenu();
        _gameUI.ShowMainMenu();
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnDestroy()
    {
        _holdEscapeHandler.DeSubscribe(ResolveExitPath);
    }
}
