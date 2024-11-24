using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimerDisplay : MonoBehaviour
{
    private Image _image;
    private Tween _tween;
    private void OnEnable()
    {
        if (_image != null)
        {
            _image.fillAmount = 1;
        } 
    }

    public void Initialize()
    {
        _image = GetComponent<Image>();
    }

    public void SetEnable(bool value)
    {
        enabled = value;
    }

    public void StartFillAmount(float value)
    {
        enabled = true;
        _tween = _image.DOFillAmount(0, value).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}
