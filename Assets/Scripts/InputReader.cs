using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField, Range(0, 2)] private int _toggleMouseButton = 0;
    [SerializeField] private KeyCode _resetKey = KeyCode.R;
    [SerializeField] private bool _isLogginInputEvents = false;

    public event Action ToggleCountingRequested;
    public event Action ResetRequested;

    private void Update()
    {
        HandleMouseInput();
        HandleKeyboardInput();
    }

    private void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(_toggleMouseButton))
        {
            ToggleCountingRequested?.Invoke();

            if (_isLogginInputEvents)
                Debug.Log($"[InputReader] Toggle counting requested via mouse button {_toggleMouseButton}.");
        }
    }

    private void HandleKeyboardInput()
    {
        if(Input.GetKeyDown(_resetKey))
        {
            ResetRequested?.Invoke();

            if (_isLogginInputEvents)
                Debug.Log($"[InputReader] Reset requested via {_resetKey} key.");
        }
    }
}