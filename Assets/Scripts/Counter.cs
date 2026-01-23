using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [Header("Настройка счетчика")]
    [SerializeField] private float _interval = 0.5f;
    [SerializeField] private bool _startOnAwake = false;

    [Header("Текущее состояние")]
    [SerializeField] private int _currentCount = 0;
    [SerializeField] private bool _isCounting = false;

    private Coroutine _countingCoroutine;

    public int CurrentCount => _currentCount;
    public bool IsCounting => _isCounting;

    private void Start()
    {
        if (_startOnAwake)
            StartCounting();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ToggleCounting();
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

        Debug.Log($"Счетчик запущен. Текущее значение: {_currentCount}");
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
        
        Debug.Log($"Счетчик остановлен. Итоговое значение: {_currentCount}");
    }

    private IEnumerator CountingRoutine()
    {
        while(_isCounting)
        {
            yield return new WaitForSeconds(_interval);

            _currentCount++;
            Debug.Log($"Счетчик: {_currentCount}");

            OnCountChanged?.Invoke(_currentCount);
        }
    }

    public void ResetCounter()
    {
        _currentCount = 0;
        Debug.Log("Счетчик сброшен до 0");

        OnCountChanged?.Invoke(_currentCount);
    }

    public event System.Action<int> OnCountChanged;
}
