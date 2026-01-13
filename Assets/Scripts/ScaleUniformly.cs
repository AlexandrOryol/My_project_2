using UnityEngine;

public class ScaleUniformly : MonoBehaviour
{
    public float ScaleSpeed = 0.3f;
    
    private void Update()
    {
        transform.localScale += Vector3.one * ScaleSpeed * Time.deltaTime;
    }
}