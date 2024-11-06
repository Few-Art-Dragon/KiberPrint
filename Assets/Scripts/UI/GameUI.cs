using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UISlideAnimation[] _mainMenu;
    [SerializeField] private UISlideAnimation _letterText;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _resultText;
    [SerializeField] private Button _startGameButton;

    private void SetMainMenuVisibility(bool show)
    {
        _startGameButton.interactable = show;
        foreach (var item in _mainMenu)
        {
            item.SetSlide(show);
        }
    }

    public void ShowMainMenu() => SetMainMenuVisibility(true);
    public void HideMainMenu() => SetMainMenuVisibility(false);

    private void SetResultMenuVisibility(bool show)
    {
        _letterText.SetSlide(show);
        _resultPanel.SetActive(show);
        _resultText.SetActive(show);
    }

    public void ShowResultMenu() => SetResultMenuVisibility(true);
    public void HideResultMenu() => SetResultMenuVisibility(false);
}
