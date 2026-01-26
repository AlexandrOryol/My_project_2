using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private Counter _counter;
    [SerializeField] private InputReader _inputReader;
    
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _statusText;

    [Header("Форматирование")]
    [SerializeField] private string _valueFormat = "Value: {0}";
    [SerializeField] private string _statusFormat = "STATUS: {0}";
    [SerializeField] private Color _countingColor = Color.green;
    [SerializeField] private Color _stoppedColor = Color.red;
    
    [Header("Настройка анимации")]
    [SerializeField] private float _pulseScale = 1.2f;
    [SerializeField] private float _pulseDuration = 0.1f;
    
    private bool _isPulsing = false;
    private Vector3 _originalScale;
    
    private void OnValidate()
    {
        if(_counter == null)
            _counter = FindObjectOfType<Counter>();
            
        if(_inputReader == null)
            _inputReader = FindObjectOfType<InputReader>();
    }
    
    private void Awake()
    {
        if(_valueText != null)
            _originalScale = _valueText.transform.localScale;
    }
    
    private void OnEnable()
    {
        if(_counter != null)
        {
            _counter.CountChanged += CountChanged;
            _counter.CountingStarted += OnCountingStarted;
            _counter.CountingStopped += OnCountingStopped;
            
            UpdateValueDisplay(_counter.CurrentValue);
            UpdateStatusDisplay(_counter.IsCounting);
        }
    }
    
    private void OnDisable()
    {
        if(_counter != null)
        {
            _counter.CountChanged -= CountChanged;
            _counter.CountingStarted -= OnCountingStarted;
            _counter.CountingStopped -= OnCountingStopped;
        }
    }

    private void Update()
    {
        if(_isPulsing && _valueText != null)
        {
            float tick = Mathf.PingPong(Time.time * (1f / _pulseDuration), 1f);
            _valueText.transform.localScale = Vector3.Lerp(_originalScale, _originalScale * _pulseScale, tick);
            
            if(!_counter.IsCounting || tick <= 0.01f)
            {
                _valueText.transform.localScale = _originalScale;
                _isPulsing = false;
            }
        }
    }
    
    private void CountChanged(int value)
    {
        UpdateValueDisplay(value);
        TriggerPulse();
    }
    
    private void OnCountingStarted()
    {
        UpdateStatusDisplay(true);
    }
    
    private void OnCountingStopped()
    {
        UpdateStatusDisplay(false);
    }
    
    private void UpdateValueDisplay(int value)
    {
        if(_valueText != null)
            _valueText.text = string.Format(_valueFormat, value);
    }
    
    private void UpdateStatusDisplay(bool isCounting)
    {
        if(_statusText != null)
        {
            string status = isCounting ? "ПОДСЧЕТ" : "ОСТАНОВЛЕНО";
            _statusText.text = string.Format(_statusFormat, status);
            _statusText.color = isCounting ? _countingColor : _stoppedColor;
        }
        
        if(_valueText != null)
        {
            _valueText.color = isCounting ? _countingColor : _stoppedColor;
        }
    }
    
    private void TriggerPulse()
    {
        _isPulsing = true;
    }
}