using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeSettings _settings;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField, Min(1)] private int _initialPoolSize = 20;

    private Queue<GameObject> _cubePool = new Queue<GameObject>();
    private Transform _poolContainer;
    private bool _isInitialized = false;

    public event Action<CubeController> CubeSpawned;
    public event Action<CubeController> CubeReturnedToPool;

    public CubeSettings Settings => _settings;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (_isInitialized)
            return;

        _poolContainer = new GameObject("CubePool").transform;
        _poolContainer.SetParent(transform);

        InitializePool();

        if (_settings == null)
        {
            throw new NullReferenceException($"{nameof(CubeSpawner)}: CubeSettings not assigned!");
        }

        if (_cubePrefab == null)
        {
            throw new NullReferenceException($"{nameof(CubeSpawner)}: CubePrefab not assigned!");
        }

        _isInitialized = true;
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNewCube();
        }
    }

    private void CreateNewCube()
    {
        GameObject cube = Instantiate(_cubePrefab, _poolContainer);
        cube.SetActive(false);

        var controller = cube.GetComponent<CubeController>();

        if (controller != null)
            controller.SetSpawner(this);

        _cubePool.Enqueue(cube);
    }

    public GameObject[] SpawnCubes(int count, Vector3 position, int generation, float splitChance, Vector3 scale)
    {
        if (!_isInitialized)
            Initialize();

        List<GameObject> spawnedCubes = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject cube = GetCubeFromPool();

            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 2f;
            cube.transform.position = position + randomOffset;
            cube.transform.rotation = Quaternion.identity;

            CubeData cubeData = cube.GetComponent<CubeData>();
            cubeData.Initialize(generation, splitChance, scale);

            RandomColorizer colorizer = cube.GetComponent<RandomColorizer>();

            if (colorizer != null)
                colorizer.ApplyRandomColor();

            cube.SetActive(true);
            spawnedCubes.Add(cube);

            var controller = cube.GetComponent<CubeController>();

            if (controller != null)
                CubeSpawned?.Invoke(controller);
        }

        return spawnedCubes.ToArray();
    }

    private GameObject GetCubeFromPool()
    {
        if (_cubePool.Count == 0)
            CreateNewCube();

        return _cubePool.Dequeue();
    }

    public void ReturnCubeToPool(GameObject cube)
    {
        if (cube == null)
            return;

        var controller = cube.GetComponent<CubeController>();

        if (controller != null)
            CubeReturnedToPool?.Invoke(controller);

        Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        cube.transform.rotation = Quaternion.identity;
        cube.SetActive(false);
        cube.transform.SetParent(_poolContainer);
        _cubePool.Enqueue(cube);
    }

    public void SpawnInitialCubes(int count)
    {
        if (!_isInitialized)
            Initialize();

        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(-5f, 5f),
                UnityEngine.Random.Range(5f, 10f),
                UnityEngine.Random.Range(-5F, 5F)
            );

            GameObject cube = GetCubeFromPool();
            cube.transform.position = randomPosition;
            cube.transform.rotation = Quaternion.identity;

            CubeData cubeData = cube.GetComponent<CubeData>();
            cubeData.Initialize(0, _settings.InitialSplitChance, Vector3.one * _settings.InitialScale);

            RandomColorizer colorizer = cube.GetComponent<RandomColorizer>();

            if (colorizer != null)
                colorizer.ApplyRandomColor();

            cube.SetActive(true);

            var controller = cube.GetComponent<CubeController>();

            if (controller != null)
                CubeSpawned?.Invoke(controller);
        }
    }
}