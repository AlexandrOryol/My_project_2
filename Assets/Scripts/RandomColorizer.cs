using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class RandomColorizer : MonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void ApplyRandomColor()
    {
        if (_renderer == null)
            return;

        Color randomColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );

        _renderer.GetPropertyBlock(_propertyBlock );
        _propertyBlock.SetColor("_Color", randomColor);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
