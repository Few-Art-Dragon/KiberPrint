using UnityEngine;

public class LetterGenerator : MonoBehaviour
{
    private const string ENG_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string RUS_ALPHABET = "ÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞß";

    private GameParameters _gameParameters;

    public delegate void LetterGeneratedHandler();
    public event LetterGeneratedHandler OnLetterGenerated;

    public char currentLetter { get; private set; }
    private char _prevLetter;

    public void Initialize(GameParameters gameParameters)
    {
        KeyboardInputHandler.OnKeyPressed += GenerateNewLetter;
        _gameParameters = gameParameters;
        GenerateNewLetter();
        _prevLetter = currentLetter;
    }

    public void GenerateNewLetter()
    {

        currentLetter = GetRandomLetter(_gameParameters.isEnglishAlphabet ? ENG_ALPHABET : RUS_ALPHABET);
        if (currentLetter == _prevLetter)
        {
            GenerateNewLetter();
        }
        else
        {
            _prevLetter = currentLetter;
            OnLetterGenerated?.Invoke();
        }
    }

    private char GetRandomLetter(string alphabet)
    {
        int randomIndex = Random.Range(0, alphabet.Length);
        return alphabet[randomIndex];
    }

    private void OnDisable()
    {
        KeyboardInputHandler.OnKeyPressed -= GenerateNewLetter;
    }

    private void OnDestroy()
    {
        KeyboardInputHandler.OnKeyPressed -= GenerateNewLetter;
    }

}
