using UnityEngine;

public class Movement : MonoBehaviour
{
    private readonly string Horizontal = "Horizontal";
    private readonly string Vertical = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private Transform _body;

    private void Update()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        Vector3 direction = (_body.right * horizontal + _body.forward * vertical).normalized;

        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}
