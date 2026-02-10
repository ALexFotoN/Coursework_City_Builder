using UnityEngine;

public class CarLookController : MonoBehaviour
{
    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.forward = transform.position - _lastPosition;
        _lastPosition = transform.position;
    }
}
