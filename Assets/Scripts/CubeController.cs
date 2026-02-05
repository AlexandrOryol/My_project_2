using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CubeData))]

public class CubeController : MonoBehaviour
{
    [SerializeField] private CubeSettings _settings;
    
    private CubeData _cubeData;
    private CubeSpawner _spawner;
    private ExplosionForce _explosionForce;
    private RandomColorizer _colorizer;
    private bool _isInitialized = false;
    
    public CubeData Data => _cubeData;
    
    private void Awake()
    {
        _cubeData = GetComponent<CubeData>();
        _colorizer = GetComponent<RandomColorizer>();
    }

    private void Start()
    {
        Initialize();
    }

    public void SetSpawner(CubeSpawner spawner)
    {
        _spawner = spawner;
    }

    private void Initialize()
    {
        if (_isInitialized)
            return;

        if (_settings == null)
        {
            Debug.LogError($"{nameof(CubeController)}: CubeSettings not assigned!");
            enabled = false;
            return;
        }

        if (_spawner == null)
            _spawner = FindObjectOfType<CubeSpawner>();

        if (_spawner == null)
        {
            Debug.LogError($"{nameof(CubeController)}: CubeSpawner not found!");
            enabled = false;
            return;
        }

        _explosionForce = FindObjectOfType<ExplosionForce>();
        _isInitialized = true;
    }
    
    private void OnMouseDown()
    {
        HandleCubeClick();
    }
    
    private void HandleCubeClick()
    {
        if (!_isInitialized)
            return;

        if(_cubeData.ShouldSplit() && CanSplitFurther())
            SplitCube();
        else
            DestroyCube();
    }
    
    private bool CanSplitFurther()
    {
        if (_settings == null)
            return false;

        float nextScale = _cubeData.CurrentScale.x * _settings.ScaleReductionFactor;
        return nextScale >= _settings.MinScale;
    }
    
    private void SplitCube()
    {
        if (_spawner == null || _settings == null)
            return;

        int splitCount = Random.Range(_settings.MinSplitCount, _settings.MaxSplitCount + 1);
        
        GameObject[] newCubes = _spawner.SpawnCubes(
            splitCount,
            transform.position,
            _cubeData.Generation + 1,
            _cubeData.SplitChance * _settings.SplitChanceReduction,
            _cubeData.CurrentScale * _settings.ScaleReductionFactor
            );
            
            if(_explosionForce != null)
                _explosionForce.ApplyExplosion(newCubes, transform.position);
                
            DestroyCube();    
    }
    
    private void DestroyCube()
    {
        if (_spawner == null)
            return;

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        _spawner.ReturnCubeToPool(gameObject);
    }
}