using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerViewMainMenu : MonoBehaviour
{
    [SerializeField] private Button _upArrow;
    [SerializeField] private Button _downArrow;
    [SerializeField] private TMP_InputField _inputField;

    private GameTimer _gameTimer;
    private const float TIME_ADJUSTMENT = 60f; 

    public void Initialize(GameTimer gameTimer)
    {
        _gameTimer = gameTimer;

        _upArrow.onClick.AddListener(IncreaseTime);
        _downArrow.onClick.AddListener(DecreaseTime);

        _inputField.onEndEdit.AddListener(UpdateMatchTimeFromInput);
        UpdateText(); 
    }

    private void IncreaseTime()
    {
        _gameTimer.AdjustMatchTime(TIME_ADJUSTMENT);
        UpdateText();
    }

    private void DecreaseTime()
    {
        _gameTimer.AdjustMatchTime(-TIME_ADJUSTMENT);
        UpdateText();
    }

    private void UpdateMatchTimeFromInput(string input)
    {
        _gameTimer.UpdateMatchTime(input);
        UpdateText();
    }

    private void UpdateText()
    {
        _inputField.text = (_gameTimer.MatchTimeInSeconds / 60).ToString("0");
    }

    private void OnDestroy()
    {
        _upArrow.onClick.RemoveListener(IncreaseTime);
        _downArrow.onClick.RemoveListener(DecreaseTime);
        _inputField.onEndEdit.RemoveListener(UpdateMatchTimeFromInput);
    }
}
