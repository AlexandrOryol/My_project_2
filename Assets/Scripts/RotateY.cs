using UnityEngine;

public class RotateY : MonoBehaviour
{
    public float rotationSpeed = 90f;
    
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}