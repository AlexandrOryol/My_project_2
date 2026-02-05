using UnityEngine;

public class CubeData : MonoBehaviour
{
    [SerializeField] private int _generation = 0;
    [SerializeField] private float _splitChance = 1f;
    [SerializeField] private Vector3 _currentScale = Vector3.one;

    public int Generation => _generation;
    public float SplitChance => _splitChance;
    public Vector3 CurrentScale => _currentScale;

    public void Initialize(int generation, float splitChance, Vector3 scale)
    {
        _generation = generation;
        _splitChance = splitChance;
        _currentScale = scale;

        transform.localScale = _currentScale;
    }

    public void UpdateForNextGeneration(float splitChanceReduction, float scaleReduction)
    {
        _generation++;
        _splitChance *= splitChanceReduction;
        _currentScale *= scaleReduction;

        transform.localScale = _currentScale;
    }

    public bool ShouldSplit()
    {
        return Random.value <= _splitChance;
    }
}
