using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private CubeSettings _settings;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField, Min(1)] private int _initialCubesCount = 5;

    private void Awake()
    {
        if (_cubeSpawner != null)
            _cubeSpawner.Initialize();
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        if( _settings == null)
        {
            throw new NullReferenceException($"{nameof(GameInitializer)}: CubeSettings is null!");
        }

        if(_cubeSpawner == null)
        {
            throw new NullReferenceException($"{nameof(GameInitializer)}: CubeSpawner is null!");
        }

        _cubeSpawner.SpawnInitialCubes(_initialCubesCount);
    }
}
