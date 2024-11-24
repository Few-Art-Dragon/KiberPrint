using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private const float MINIMUM_MATCH_TIME = 60f;
    private const float MAXIMUM_MATCH_TIME = 540f;
    private const float DEFAULT_MATCH_TIME = 120f;

    public float MatchTimeInSeconds { get; private set; } = DEFAULT_MATCH_TIME;

    private Coroutine _letterTimerCoroutine;
    private Coroutine _matchTimerCoroutine;

    public void StartMatchTimer(float duration, System.Action onEnd)
    {
        RestartCoroutine(ref _matchTimerCoroutine, duration, onEnd);
    }

    public void StartLetterTimer(float duration, System.Action onEnd, bool isGameOver)
    {
        if (!isGameOver)
        {
            RestartCoroutine(ref _letterTimerCoroutine, duration, onEnd);
        }
    }

    public void StopAllTimers()
    {
        StopAllCoroutines();
        _letterTimerCoroutine = null;
        _matchTimerCoroutine = null;
    }

    public void AdjustMatchTime(float adjustmentInSeconds)
    {
        MatchTimeInSeconds = Mathf.Clamp(MatchTimeInSeconds + adjustmentInSeconds, MINIMUM_MATCH_TIME, MAXIMUM_MATCH_TIME);
    }

    public void UpdateMatchTime(string input)
    {
        if (float.TryParse(input, out float minutes) && minutes >= 1f && minutes <= 9f)
        {
            MatchTimeInSeconds = minutes * 60f;
        }
        else
        {
            MatchTimeInSeconds = DEFAULT_MATCH_TIME;
        }
    }

    private void RestartCoroutine(ref Coroutine coroutine, float duration, System.Action onEnd)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(TimerCoroutine(duration, onEnd));
    }

    private IEnumerator TimerCoroutine(float duration, System.Action onEnd)
    {
        yield return new WaitForSeconds(duration);
        onEnd?.Invoke();
    }
}
