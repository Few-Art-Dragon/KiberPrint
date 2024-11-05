using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitcherButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;
    [SerializeField] private float _transitionDuration = 0.03f;

    private bool _isFirstToThird = true;
    private bool _isButtonLocked = false;

    public void OnSwitcherClick()
    {
        if (_isButtonLocked) return; 

        _isButtonLocked = true;

        Sequence switchSequence = DOTween.Sequence();
        switchSequence.AppendCallback(() => _buttonImage.sprite = _isFirstToThird ? _sprite1 : _sprite3)
                      .AppendInterval(_transitionDuration)
                      .AppendCallback(() => _buttonImage.sprite = _sprite2)
                      .AppendInterval(_transitionDuration)
                      .AppendCallback(() => _buttonImage.sprite = _isFirstToThird ? _sprite3 : _sprite1)
                      .OnComplete(() => _isButtonLocked = false);

        _isFirstToThird = !_isFirstToThird;
    }
}
