using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrapper : MonoBehaviour
{
    private enum GameState
    {
        Playing,
        MainMenu
    }
    [SerializeField] private ThirdSwitcherButton _difficultyButton;
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
    [SerializeField] private HoldKeyHandler _holdEscapeHandler;
    [SerializeField] private SecretMode _triggerSecretMode;
    [SerializeField] private TimerDisplay _timerDisplay;

    private GameState _currentState;

    private void Awake()
    {
        InitializeMainMenu();
        _currentState = GameState.MainMenu;
    }

    private void InitializeMainMenu()
    {
        _timerDisplay.Initialize();
        _difficultyButton.Initialize(_difficultySettings);
        _gameUI.ShowMainMenu(_gameParameters.isTrainingMode);
        SubscribeToEvents();
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _gameParameters.Initialize();
        _difficultySettings.Initialize();
        _scoreBalance.Initialize();
        _keyboardInputHandler.Initialize(_gameParameters, _letterGenerator, _scoreBalance, _scoreDisplay, _gameTimer, _difficultySettings);
        _timerViewMainMenu.Initialize(_gameTimer);
    }

    public void StartGame()
    {
        _gameUI.HideMainMenu(_gameParameters.isTrainingMode);
        _currentState = GameState.Playing;

        InitializeGameComponents();
        StartGameTimers();
    }

    private void InitializeGameComponents()
    {
        _letterDisplay.Initialize(_letterGenerator);
        _letterGenerator.Initialize(_gameParameters);
        _scoreDisplay.Initialize(_gameParameters);
        _gameParameters.SetGameOver(false);
    }

    private void StartGameTimers()
    {
        if (_gameParameters.isTrainingMode)
        {
            _timerDisplay.SetEnable(false);
            return;
        }

        _letterGenerator.OnLetterGenerated += RestartLetterTimer;
        _gameTimer.StartMatchTimer(_gameTimer.MatchTimeInSeconds, EndGame);
        StartLetterTimer();
        _timerDisplay.SetEnable(true);
        _timerDisplay.StartFillAmount(_gameTimer.MatchTimeInSeconds);
    }

    private void StartLetterTimer()
    {
        _gameTimer.StartLetterTimer(_difficultySettings.GetLetterChangeTime(), _letterGenerator.GenerateNewLetter, false);
    }

    private void RestartLetterTimer()
    {
        StartLetterTimer();
    }

    public void EndGame()
    {
        _scoreDisplay.ResetText("");
        FinalizeGame();
        _gameUI.ShowResultMenu();
        _letterDisplay.UpdateText(_scoreBalance.Score);
    }

    public void RestartGame()
    {
        ResetGame();
        StartNewMatch();
    }

    private void ResetGame()
    {
        _scoreDisplay.ResetText();
        _scoreBalance.Initialize();
        _gameTimer.StopAllTimers();
        _gameUI.HideResultMenu();
    }

    private void StartNewMatch()
    {
        StartLetterTimer();
        StartGameTimers();
        _gameParameters.SetGameOver(false);
        _letterGenerator.GenerateNewLetter();
        _timerDisplay.enabled = false;
        _timerDisplay.StartFillAmount(_gameTimer.MatchTimeInSeconds);
    }

    private void FinalizeGame()
    {
        _gameTimer.StopAllTimers();
        _gameParameters.SetGameOver(true);
        _letterGenerator.OnLetterGenerated -= RestartLetterTimer;
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

    private void ResolveSecretModePath()
    {
        if (_currentState == GameState.MainMenu)
        {

            _difficultySettings.SetDifficultyLevel();
            _difficultyButton.OnSwitcherClick();
        }
    }

    private void ExitToMainMenu()
    {
        FinalizeGame();
        _currentState = GameState.MainMenu;
        ResetUI();
        _gameUI.ShowMainMenu(_gameParameters.isTrainingMode);
    }

    private void ResetUI()
    {
        _timerDisplay.enabled = false;
        _letterDisplay.UpdateText("");
        _scoreDisplay.ResetText();
        _scoreBalance.Initialize();
        _gameUI.HideResultMenu();
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void SubscribeToEvents()
    {
        _holdEscapeHandler.Subscribe(ResolveExitPath);
        _triggerSecretMode.Subscribe(ResolveSecretModePath);
        //_triggerSecretMode.Subscribe(_difficultyButton.SetSecretButton);
    }


    private void UnsubscribeFromEvents()
    {
        _holdEscapeHandler.Unsubscribe(ResolveExitPath);
        _triggerSecretMode.Unsubscribe(ResolveSecretModePath);
        //_triggerSecretMode.Unsubscribe(_difficultyButton.SetSecretButton);
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
