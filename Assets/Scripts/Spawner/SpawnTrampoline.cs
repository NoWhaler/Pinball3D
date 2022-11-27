using CollisionDetection;
using UnityEngine;
using View;

namespace DefaultNamespace
{
    public class SpawnTrampoline : MonoBehaviour
    {
        private CollisionDetector _trampoline;

        private void Start()
        {
            _trampoline = GetComponentInChildren<CollisionDetector>();
            _trampoline.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            var ball = other.GetComponent<BallView>();
            if (ball != null)
            {
                _trampoline.gameObject.SetActive(true);
            }
        }
    }
}