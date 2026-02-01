using TMPro;
using UnityEngine;

public class CounterDisplay : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Counter _counter;
    
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _statusText;

    [Header("Display Configuration")]
    [SerializeField] private string _countFormat = "Value: {0}";
    [SerializeField] private string _statusFormat = "STATUS: {0}";
    [SerializeField] private Color _countingColor = Color.green;
    [SerializeField] private Color _stoppedColor = Color.red;

    private void Awake()
    {
        ValidateReferences();
    }

    private void OnEnable()
    {
        SubscribeToCounterEvents();
        UpdateDisplay(_counter.CurrentCount, _counter.IsCounting);
    }
    
    private void OnDisable()
    {
        UnsubscribeFromCounterEvents();
    }

    private void ValidateReferences()
    {
        if(_counter == null)
        {
            Debug.LogError($"{nameof(CounterDisplay)}: Counter reference is mandatory" +
                $"Assign in inspector or ensure Counter component exists in scene.");
            enabled = false;
        }

        if(_countText == null)
        {
            Debug.LogError($"{nameof(CounterDisplay)}: Count text reference is required.");
        }
    }

    private void SubscribeToCounterEvents()
    {
        if (_counter == null)
            return;

        _counter.CountChanged += HandleCountChanged;
    }

    private void UnsubscribeFromCounterEvents()
    {
        if (_counter == null)
            return;

        _counter.CountChanged -= HandleCountChanged;
    }

    private void HandleCountChanged(int count)
    {
        UpdateDisplay(count, _counter.IsCounting);
    }
    
    private void UpdateDisplay(int count, bool isCounting)
    {
        UpdateCountDisplay(count);
        UpdateStatusDisplay(isCounting);
    }
    
    private void UpdateCountDisplay(int count)
    {
        if(_countText == null)
            return;
            
        _countText.text = string.Format(_countFormat, count);
        _countText.color = _counter.IsCounting ? _countingColor : _stoppedColor;
    }   
    
    private void UpdateStatusDisplay(bool isCounting)
    {
        if(_statusText == null)
            return;
            
        string status = GetStatusString(_counter.IsCounting);
        _statusText.text = string.Format(_statusFormat, status);
        _statusText.color = _counter.IsCounting ? _countingColor : _stoppedColor;
    }

    private string GetStatusString(bool isCounting)
    {
        return isCounting ? "COUNTING" : "STOPPED";
    }
}