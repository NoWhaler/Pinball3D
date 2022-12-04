using UnityEngine;

namespace View
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float _force;
        
        private void OnCollisionEnter(Collision other)
        {
            var ball = other.collider.GetComponent<BallView>();

            if (ball != null)
            {
                PushBall(other.rigidbody);
            }
        }

        private void PushBall(Rigidbody rigidbody)
        {
            rigidbody.AddForce(transform.forward * _force, ForceMode.Acceleration);
        }
    }
}