using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime;

    private Vector3 _velocity = Vector3.zero;

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, 0, _ball.position.z) + _offset,
            ref _velocity, _smoothTime);
    }
}
