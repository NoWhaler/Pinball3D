using UnityEngine;

namespace View
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float _force;
        
        private void OnTriggerEnter(Collider other)
        {
            var ball = other.GetComponent<BallView>();

            if (ball != null)
            {
                PushBall(other.attachedRigidbody);
            }
        }

        private void PushBall(Rigidbody rigidbody)
        {
            rigidbody.AddForce(transform.forward * _force, ForceMode.Acceleration);
        }
    }
}