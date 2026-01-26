using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public event Action<int> CountChanged;
    public event Action CountingStarted;
    public event Action CountingStopped;
    
    [Header("Настройка счетчика")]
    [SerializeField] private float _interval = 0.5f;

    [Header("Текущее состояние")]
    [SerializeField] private int _currentValue = 0;
    [SerializeField] private bool _isCounting = false;

    private Coroutine _countingCoroutine;
    private WaitForSeconds _waitInterval;

    public int CurrentValue => _currentValue;
    public bool IsCounting => _isCounting;
    
    private void Awake()
    {
        _waitInterval = new WaitForSeconds(_interval);
    }
    
    private void OnEnable()
    {
        var inputReader = FindObjectOfType<InputReader>();
        
        if(inputReader != null)
        {
            inputReader.MouseLeftClicked += ToggleCounting;
            inputReader.ResetKeyPressed += ResetCounter;
        }
    }
    
    private void OnDisable()
    {
        var inputReader = FindObjectOfType<InputReader>();
        
        if(inputReader != null)
        {
            inputReader.MouseLeftClicked -= ToggleCounting;
            inputReader.ResetKeyPressed -= ResetCounter;
        }
    }

    public void ToggleCounting()
    {
        if (_isCounting)
            StopCounting();
        else
            StartCounting();
    }

    public void StartCounting()
    {
        if(_isCounting)
            return;

        _isCounting = true;
        _countingCoroutine = StartCoroutine(CountingRoutine());
        
        CountingStarted?.Invoke();
    }

    public void StopCounting()
    {
        if(!_isCounting)
            return;

        _isCounting = false;

        if( _countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
            _countingCoroutine = null;
        }
        
        CountingStopped?.Invoke();
    }

    private IEnumerator CountingRoutine()
    {
        while(_isCounting)
        {
            yield return _waitInterval;

            _currentValue++;

            CountChanged?.Invoke(_currentValue);
        }
    }

    public void ResetCounter()
    {
        _currentValue = 0;

        CountChanged?.Invoke(_currentValue);
    }
}