using System;
using UnityEngine;

namespace View
{
    public class MovableObject : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var ball = collision.rigidbody.GetComponent<BallView>();
            if (ball != null)
            { 
                _rigidbody.AddRelativeTorque(new Vector3(0,0, collision.rigidbody.velocity.z), ForceMode.Impulse);
            }
        }
    }
}