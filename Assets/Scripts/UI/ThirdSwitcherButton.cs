using UnityEngine;
using UnityEngine.UI;

public class ThirdSwitcherButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;

    private int _currentState = 0;

    public void OnSwitcherClick()
    {
        _currentState = (_currentState + 1) % 3;

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
        }
    }
}
