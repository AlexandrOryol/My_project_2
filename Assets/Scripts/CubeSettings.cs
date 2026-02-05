using UnityEngine;

[CreateAssetMenu(fileName = "CubeSettings", menuName = "Cube Explosion/Cube Settings")]

public class CubeSettings : ScriptableObject
{
    [Header("Spawning Settings")]
    [SerializeField, Min(2)] private int _minSplitCount = 2;
    [SerializeField, Min(2)] private int _maxSplitCount = 6;
    [SerializeField, Min(0.1f)] private float _initialScale = 1f;
    [SerializeField, Min(0.1f)] private float _scaleReductionFactor = 0.5f;
    [SerializeField, Min(0.1f)] private float _minScale = 0.1f;
    
    [Header("Explosion Settings")]
    [SerializeField, Min(0f)] private float _explosionForce = 10f;
    [SerializeField, Min(0f)] private float _explosionRadius = 5f;
    [SerializeField] private ForceMode _explosionForceMode = ForceMode.Impulse;
    
    [Header("Split Chance Settings")]
    [SerializeField, Range(0f, 1f)] private float _initialSplitChance = 1f;
    [SerializeField, Range(0f, 1f)] private float _splitChanceReduction = 0.5f;
    
    [Header("Boundary Settings")]
    [SerializeField] private float _boundarySize = 20f;
    
    public int MinSplitCount => _minSplitCount;
    public int MaxSplitCount => _maxSplitCount;
    public float InitialScale => _initialScale;
    public float ScaleReductionFactor => _scaleReductionFactor;
    public float MinScale => _minScale;
    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    public ForceMode ExplosionForceMode => _explosionForceMode;
    public float InitialSplitChance => _initialSplitChance;
    public float SplitChanceReduction => _splitChanceReduction;
    public float BoundarySize => _boundarySize;
}