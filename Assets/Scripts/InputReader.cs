using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action MouseLeftClicked;
    public event Action ResetKeyPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseLeftClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.R))
            ResetKeyPressed?.Invoke();
    }
}