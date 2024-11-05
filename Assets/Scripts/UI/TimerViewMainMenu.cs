using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerViewMainMenu : MonoBehaviour
{
    [SerializeField] private Button _upArrow;
    [SerializeField] private Button _downArrow;

    [SerializeField] private TMP_InputField _inputField;

    private GameTimer _gameTimer;

    public void Initialize(GameTimer gameTimer)
    {
        _gameTimer = gameTimer;

        _upArrow.onClick.AddListener(gameTimer.AddTime);
        _downArrow.onClick.AddListener(gameTimer.SubtractTime);
        _upArrow.onClick.AddListener(UpdateText);
        _downArrow.onClick.AddListener(UpdateText);

        _inputField.onEndEdit.AddListener(gameTimer.UpdateMatchTime);
    }

    private void UpdateText()
    {
        _inputField.text = (_gameTimer.MatchTimeInSeconds / 60).ToString();
    }

    private void OnDestroy()
    {
        _upArrow.onClick.RemoveAllListeners();
        _downArrow.onClick.RemoveAllListeners();
        _inputField.onEndEdit.RemoveAllListeners();
    }
}
