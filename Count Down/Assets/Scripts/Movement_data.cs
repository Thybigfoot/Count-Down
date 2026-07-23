using UnityEngine;

public class Movement_data : MonoBehaviour
{
    public Vector2 DeltaMovement { get; private set; }

    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        DeltaMovement = (Vector2)(transform.position - _lastPosition);
        _lastPosition = transform.position;
    }
}
