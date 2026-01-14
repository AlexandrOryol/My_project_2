using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float Speed = 5f;

    public enum MoveDirection { WorldForward, LocalForward, LocalBack, Right, Up };
    public MoveDirection Direction = MoveDirection.LocalForward;

    private void Update()
    {
        Vector3 moveVector = GetMoveDirection();
        transform.position += moveVector * Speed * Time.deltaTime;
    }

    private Vector3 GetMoveDirection()
    {
        switch (Direction)
        {
            case MoveDirection.WorldForward:
                return Vector3.forward;

            case MoveDirection.LocalForward:
                return transform.forward;

            case MoveDirection.LocalBack:
                return -transform.forward;

            case MoveDirection.Right:
                return transform.right;

            case MoveDirection.Up:
                return transform.up;

            default:
                return transform.forward;
        }
    }
}