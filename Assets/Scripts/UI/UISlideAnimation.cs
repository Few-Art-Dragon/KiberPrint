using UnityEngine;
using DG.Tweening;

public class UISlideAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _uiElement;  
    [SerializeField] private Vector2 _targetPosition;   
    [SerializeField] private float _duration = 0.5f;    
    [SerializeField] private bool _startAnim = false;

    private Vector2 _initialPosition;
    private void OnEnable()
    {
        if (_startAnim)
        {
            SlideToTarget();
        }
    }
    
    private void Start()
    { 
        _initialPosition = _uiElement.anchoredPosition;
    }

    public void SetSlide(bool show)
    {
        if (show)
            SlideToTarget();
        else
            SlideToInitial();

    }

    private void SlideToTarget()
    {
        _uiElement.DOAnchorPos(_targetPosition, _duration).SetEase(Ease.InOutQuad);
    }

    private void SlideToInitial()
    {
        _uiElement.DOAnchorPos(_initialPosition, _duration).SetEase(Ease.InOutQuad);
    }
}
