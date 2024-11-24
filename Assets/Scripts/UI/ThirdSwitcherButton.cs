using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ThirdSwitcherButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3; 
    [SerializeField] private Sprite _secretModeButton;
    private DifficultySettings _difficultySettings;

    private int _currentState = 0;

    public void Initialize(DifficultySettings difficultySettings)
    {
        _difficultySettings = difficultySettings;
    }

    public void OnSwitcherClick()
    {
        _currentState = ((int)_difficultySettings.difficultyLevel);

        switch (_currentState)
        {
            case 0:
                _buttonImage.sprite = _sprite1;
                break;
            case 1:
                _buttonImage.sprite = _sprite2;
                break;
            case 2:
                _buttonImage.sprite = _sprite3;
                break;
            case 3:
                _buttonImage.sprite = _secretModeButton;
                break;
        }
    }

    public void SetSecretButton()
    {
        _buttonImage.sprite = _secretModeButton;
    }
}
