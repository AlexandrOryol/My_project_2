using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    public event UnityAction MouseLeftClicked;
    public event UnityAction ResetKeyPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseLeftClicked?.Invoke();

        if (Input.GetKeyDown(KeyCode.R))
            ResetKeyPressed?.Invoke();
    }
}