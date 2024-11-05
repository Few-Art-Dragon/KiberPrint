using TMPro;
using UnityEngine;

public class LetterDisplay : MonoBehaviour
{
    private LetterGenerator _letterGenerator;

    private TextMeshProUGUI _text;
    public void Initialize(LetterGenerator letterGenerator)
    {
        _text = GetComponent<TextMeshProUGUI>();
        _letterGenerator = letterGenerator;
        UpdateText();
        _letterGenerator.OnLetterGenerated += UpdateText;
    }

    public void UpdateText()
    {
        _text.text = _letterGenerator.currentLetter.ToString();
    }

    public void UpdateText(float value)
    {
        _text.text = value.ToString();
    }

    public void UpdateText(string value)
    {
        _text.text = value;
    }

    private void OnDestroy()
    {
        if (_letterGenerator != null)
        {
            _letterGenerator.OnLetterGenerated -= UpdateText;
        }  
    }

}
