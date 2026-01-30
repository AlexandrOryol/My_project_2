using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private int _mouseButton = 0;
    [SerializeField] private KeyCode _resetKey = KeyCode.R;
    [SerializeField] private bool _logInputEvents = false;

    public event Action MouseLeftClicked;
    public event Action ResetKeyPressed;

    private void Update()
    {
        HandleMouseInput();
        HandleKeuboardInput();
    }

    private void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(_mouseButton))
        {
            MouseLeftClicked?.Invoke();

            if (_logInputEvents)
                Debug.Log($"[InputReader] Mouse button {_mouseButton} clicked");
        }
    }

    private void HandleKeuboardInput()
    {
        if(Input.GetKeyDown(_resetKey))
        {
            ResetKeyPressed?.Invoke();

            if (_logInputEvents)
                Debug.Log($"[InputReader] {_resetKey} key pressed");
        }
    }
}