using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime;

    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _velocity;

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _ball.position + _offset,
            ref _velocity, _rigidbody.velocity.y * Time.deltaTime);
    }
}
