using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReader))]

public class Counter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Min(0.01f)] private float _countingInterval = 0.5f;

    private InputReader _inputReader;
    private int _currentCount;
    private bool _isCounting;
    private Coroutine _countingCoroutine;

    public event Action<int> CountChanged;

    public int CurrentCount => _currentCount;
    public bool IsCounting => _isCounting;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();

        ValidateDependencies();
    }

    private void OnEnable()
    {
        SubscribeToInputEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromInputEvents();
        StopCountingInternal();
    }

    public void ResetCount()
    {
        _currentCount = 0;

        CountChanged?.Invoke(_currentCount);
    }

    public void SetCount(int value)
    {
        if (value < 0)
        {
            Debug.LogWarning($"Attempt to set negative count: {value}");
            return;
        }

        _currentCount = value;

        CountChanged?.Invoke(_currentCount);
    }

    private void ValidateDependencies()
    {
        if (_inputReader == null)
        {
            Debug.LogError($"{nameof(Counter)}: {nameof(InputReader)} component is missing!" +
                $"Attach {nameof(InputReader)} component to the same GameObject.");
            enabled = false;
        }
    }

    private void SubscribeToInputEvents()
    {
        if (_inputReader == null)
            return;

        _inputReader.ToggleCountingRequested += HandleToggleCountingRequested;
        _inputReader.ResetRequested += HandleResetRequested;
    }

    private void UnsubscribeFromInputEvents()
    {
        if (_inputReader == null)
            return;

        _inputReader.ToggleCountingRequested -= HandleToggleCountingRequested;
        _inputReader.ResetRequested -= HandleResetRequested;
    }

    private void HandleToggleCountingRequested()
    {
        if(_isCounting)
            HandleStopCounting();
        else
            HandleStartCounting();
    }

    private void HandleResetRequested()
    {
        ResetCount();
    }

    private void HandleStartCounting()
    {
        if (_isCounting)
            return;

        _isCounting = true;
        _countingCoroutine = StartCoroutine(CountingProcess());

        CountChanged?.Invoke(_currentCount);
    }

    private void HandleStopCounting()
    {
        if (!_isCounting)
            return;

        StopCountingInternal();

        CountChanged?.Invoke(_currentCount);
    }

    private void StopCountingInternal()
    {
        _isCounting = false;

        if (_countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
            _countingCoroutine = null;
        }
    }

    private IEnumerator CountingProcess()
    {
        var waitInterval = new WaitForSeconds(_countingInterval);

        while (_isCounting)
        {
            yield return waitInterval;
            _currentCount++;
            CountChanged?.Invoke(_currentCount);
        }
    }
}