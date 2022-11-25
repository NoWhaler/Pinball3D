using UnityEngine;

namespace CollisionDetection
{
    public class CollisionDetector : MonoBehaviour
    {
        
        [SerializeField] private float _explosionForce;

        private void OnCollisionEnter(Collision collision)
        {
            collision.rigidbody.AddExplosionForce(_explosionForce, transform.position, 5);
        }
    }
}