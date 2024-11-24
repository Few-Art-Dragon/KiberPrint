using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UISlideAnimation[] _mainMenuElements;
    [SerializeField] private UISlideAnimation _letterDisplayAnimation;
    [SerializeField, Tooltip("Панель с результатами, кнопка 'Рестарт' и очки")] private GameObject _resultPanel;
    [SerializeField] private GameObject _resultText;
    [SerializeField] private GameObject _scoreText;
    [SerializeField] private GameObject _timerIcon;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private GameObject _secretMode;

    private void ConfigureTrainingMode(bool isTrainingMode)
    {
        _timerIcon.SetActive(!isTrainingMode);
    }

    private void SetMainMenuVisibility(bool isVisible, bool isTrainingMode)
    {
        _secretMode.SetActive(isVisible);
        ConfigureTrainingMode(isTrainingMode);
        _startGameButton.interactable = isVisible;
        _scoreText.SetActive(!isVisible);

        foreach (var element in _mainMenuElements)
        {
            element.SetSlide(isVisible);
        }
    }

    public void ShowMainMenu(bool isTrainingMode) => SetMainMenuVisibility(true, isTrainingMode);
    public void HideMainMenu(bool isTrainingMode) => SetMainMenuVisibility(false, isTrainingMode);

    private void SetResultMenuVisibility(bool isVisible)
    {
        _letterDisplayAnimation.SetSlide(isVisible);
        _resultPanel.SetActive(isVisible);
        _resultText.SetActive(isVisible);
    }

    public void ShowResultMenu() => SetResultMenuVisibility(true);
    public void HideResultMenu() => SetResultMenuVisibility(false);
}
