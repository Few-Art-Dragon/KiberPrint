using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldKeyHandler : MonoBehaviour
{
    [SerializeField] private InputAction holdKeyAction;

    private void OnEnable()
    {
        holdKeyAction.Enable();
    }

    public void Subscribe(Action<InputAction.CallbackContext> onStart)
    {
        holdKeyAction.performed += onStart;
    }

    public void Unsubscribe(Action<InputAction.CallbackContext> onStart)
    {
        holdKeyAction.performed -= onStart;
    }

    private void OnDisable()
    {
        holdKeyAction.Disable();
    }
}
