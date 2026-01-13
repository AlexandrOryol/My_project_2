using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float Speed = 5f;
    
    private void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
}