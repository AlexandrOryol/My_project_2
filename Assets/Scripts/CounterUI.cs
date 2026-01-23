using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [Header("—Ò˚ÎÍË")]
    [SerializeField] private Counter _counter;
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private TextMeshProUGUI _statusText;

    [Header("‘ÓÏ‡ÚËÓ‚‡ÌËÂ")]
    [SerializeField] private string _format = "—˜ÂÚ: {0}";
    [SerializeField] private Color _countingColor = Color.green;
    [SerializeField] private Color _stoppedColor = Color.red;

    private void Start()
    {
        if(_counter != null)
        {
            _counter.OnCountChanged += UpdateCounterUI;
            UpdateCounterUI(_counter.CurrentCount);
        }

        UpdateStatusUI();
    }

    private void OnDestroy()
    {
        if (_counter != null)
            _counter.OnCountChanged -= UpdateCounterUI;
    }

    private void Update()
    {
        UpdateColors();

        if(Input.GetKeyDown(KeyCode.R))
            _counter?.ResetCounter();
    }

    private void UpdateColors()
    {
        if (_counter == null)
            return;

        bool isCounting = _counter.IsCounting;
        Color targetColor = isCounting ? _countingColor : _stoppedColor;

        if (_counterText != null)
            _counterText.color = targetColor;

        if(_statusText != null)
            _statusText.color = targetColor;
    }

    private void UpdateCounterUI(int count)
    {
        if(_counterText != null)
        {
            _counterText.text = string.Format(_format, count);

            UpdateColors();
            StartCoroutine(PulseAnimation());
        }
    }

    private void UpdateStatusUI()
    {
        if(_statusText != null && _counter != null)
        {
            _statusText.text = _counter.IsCounting ? "—“¿“”—: —◊»“¿≈“" : "—“¿“”—: Œ—“¿ÕŒ¬À≈Õ";

            UpdateColors();
        }
    }

    private IEnumerator PulseAnimation()
    {
        if (_counterText == null) yield break;

        Vector3 originalScale = _counterText.transform.localScale;
        Vector3 targetScale = originalScale * 1.2f;

        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / duration;
            _counterText.transform.localScale = Vector3.Lerp(originalScale, targetScale, time);
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / duration;
            _counterText.transform.localScale = Vector3.Lerp(targetScale, originalScale, time);
            yield return null;
        }

        _counterText.transform.localScale = originalScale;
    }
}
