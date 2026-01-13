using UnityEngine;

public class MoveRotateAndGrow : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotationSpeed = 60f;
    public float ScaleSpeed = 0.3f;
    
    private void Update()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        
        transform.localScale += Vector3.one * ScaleSpeed * Time.deltaTime;
    }
}