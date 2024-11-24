using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public void Initialize(GameParameters gameParameters)
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.enabled = gameParameters.isTrainingMode? false : true;
        ResetText();
    }

    public void ResetText()
    {
        _text.text = "0";
    }

    public void ResetText(string value)
    {
        _text.text = value;
    }

    public void UpdateScore(ushort score) => _text.text = score.ToString();
}
