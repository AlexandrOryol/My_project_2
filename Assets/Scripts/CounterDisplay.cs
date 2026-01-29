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
    
    private void OnValidate()
    {
        FindCounterIfNull();
    }

    private void Awake()
    {
        ValidateReferences();
    }

    private void OnEnable()
    {
        SubscribeToCounterEvents();
        UpdateDisplay();
    }
    
    private void OnDisable()
    {
        UnsubscribeFromCounterEvents();
    }

    private void FindCounterIfNull()
    {
        if (_counter == null)
            _counter = FindObjectOfType<Counter>();
    }

    private void ValidateReferences()
    {
        if(_counter == null)
        {
            Debug.LogError($"{nameof(CounterDisplay)}: Counter reference is missing");
        }

        if(_countText == null)
        {
            Debug.LogError($"{nameof(CounterDisplay)}: Count text reference is missing");
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
        UpdateDisplay();
    }
    
    private void UpdateDisplay()
    {
        if (_counter == null)
            return;

        UpdateCountDisplay();
        UpdateStatusDisplay();
    }
    
    private void UpdateCountDisplay()
    {
        if(_countText == null)
            return;
            
        _countText.text = string.Format(_countFormat, _counter.CurrentCount);
        _countText.color = _counter.IsCounting ? _countingColor : _stoppedColor;
    }   
    
    private void UpdateStatusDisplay()
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