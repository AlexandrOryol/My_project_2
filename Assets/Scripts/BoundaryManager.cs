using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    [SerializeField] private CubeSettings _settings;
    [SerializeField] private bool _drawGizmos = true;

    private CubeSpawner _cubeSpawner;
    private readonly List<CubeController> _activeCubes = new List<CubeController>();

    private void Awake()
    {
        _cubeSpawner = FindObjectOfType<CubeSpawner>();

        ValidateDependencies();
    }

    private void OnEnable()
    {
        if (_cubeSpawner != null)
        {
            _cubeSpawner.CubeSpawned += OnCubeSpawned;
            _cubeSpawner.CubeReturnedToPool += OnCubeReturnedToPool;
        }
    }

    private void OnDisable()
    {
        if (_cubeSpawner != null)
        {
            _cubeSpawner.CubeSpawned -= OnCubeSpawned;
            _cubeSpawner.CubeReturnedToPool -= OnCubeReturnedToPool;
        }
    }

    private void ValidateDependencies()
    {
        if (_cubeSpawner == null)
        {
            Debug.LogError($"{nameof(BoundaryManager)}: CubeSpawner not found!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (_settings == null)
            return;

        foreach (var cube in _activeCubes)
        {
            if (cube != null && cube.gameObject.activeInHierarchy)
            {
                KeepCubeInBounds(cube.transform);
            }
        }
    }

    private void OnCubeSpawned(CubeController cube)
    {
        if (cube != null && !_activeCubes.Contains(cube))
            _activeCubes.Add(cube);
    }

    private void OnCubeReturnedToPool(CubeController cube)
    {
        if (cube != null)
            _activeCubes.Remove(cube);
    }

    private void KeepCubeInBounds(Transform cubeTransform)
    {
        Vector3 position = cubeTransform.position;
        float boundary = _settings.BoundarySize * 0.5f;

        position.x = Mathf.Clamp(position.x, -boundary, boundary);
        position.y = Mathf.Clamp(position.y, -boundary, boundary);
        position.z = Mathf.Clamp(position.z, -boundary, boundary);

        cubeTransform.position = position;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos || _settings == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * _settings.BoundarySize);
    }
}
