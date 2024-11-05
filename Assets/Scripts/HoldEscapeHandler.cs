using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldEscapeHandler : MonoBehaviour
{
    [SerializeField] private InputAction holdEscapeAction;

    private void OnEnable()
    {
        holdEscapeAction.Enable();
    }

    public void Subscribe(Action<InputAction.CallbackContext> onStart)
    {
        holdEscapeAction.performed += onStart;
    }

    public void DeSubscribe(Action<InputAction.CallbackContext> onStart)
    {
        holdEscapeAction.performed -= onStart;
    }

    private void OnDisable()
    {
        holdEscapeAction.Disable();
    }
}
