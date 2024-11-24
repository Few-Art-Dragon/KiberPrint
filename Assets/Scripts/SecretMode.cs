using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecretMode : MonoBehaviour
{
    public delegate void KeyCodePressedHandler();
    public event KeyCodePressedHandler OnKeyCodePressed;

    [SerializeField] private string targetSequence = "911";
    private int _currentIndex = 0; 

    private void Update()
    {
        if (Keyboard.current.digit9Key.wasPressedThisFrame)
            CheckKey("9");
        else if (Keyboard.current.digit1Key.wasPressedThisFrame)
            CheckKey("1");
        else if (Keyboard.current.anyKey.wasPressedThisFrame)
            ResetSequence();
    }

    private void CheckKey(string key)
    {
        if (key == targetSequence[_currentIndex].ToString())
        {
            _currentIndex++;

            if (_currentIndex == targetSequence.Length)
            {
                OnKeyCodePressed.Invoke();
                ResetSequence();
            }
        }
        else
        {
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        _currentIndex = 0;
    }

    public void Subscribe(KeyCodePressedHandler onStart)
    {
        OnKeyCodePressed += onStart;
    }

    public void Unsubscribe(KeyCodePressedHandler onStart)
    {
        OnKeyCodePressed -= onStart;
    }
}
