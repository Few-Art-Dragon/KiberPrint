using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private const float MIN_TIME_SEC = 60;
    private const float MAX_TIME_SEC = 540;

    public float MatchTimeInSeconds { get; private set; }

    private Coroutine _ñoroutine;

    public void Initialize()
    {
        MatchTimeInSeconds = 120f;
    }
  
    public void StartMatchTimer(float duration, System.Action onEnd)
    {
        StartCoroutine(TimerCoroutine(duration, onEnd));
    }

    public void StartLetterTimer(float duration, System.Action onEnd)
    {
        if (_ñoroutine != null)
        {
            StopCoroutine(_ñoroutine);
        }
        _ñoroutine = StartCoroutine(TimerCoroutine(duration, onEnd));
    }

    public void StopAllTimers()
    {
        StopAllCoroutines();
    }

    public void AddTime()
    {
        if (MatchTimeInSeconds < MAX_TIME_SEC)
        {
            MatchTimeInSeconds += MIN_TIME_SEC;  
        }    
    }

    public void SubtractTime()
    {
        if (MatchTimeInSeconds > MIN_TIME_SEC)
        {
            MatchTimeInSeconds -= MIN_TIME_SEC; 
        }
    }

    public void UpdateMatchTime(string input)
    {
        if (input != null && float.TryParse(input, out float minutes) && minutes >= 1 && minutes <= 9f)
        {
            MatchTimeInSeconds = minutes * 60f;
        }
        else
        {
            MatchTimeInSeconds = 120f; 
        }
    }

    private IEnumerator TimerCoroutine(float duration, System.Action onEnd)
    {
        yield return new WaitForSeconds(duration);
        onEnd.Invoke();
    }

}
