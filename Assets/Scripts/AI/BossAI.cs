using UnityEngine;

namespace AI
{
    public class BossAI : MonoBehaviour
    {
        [SerializeField] private Transform[] _waypoints;
        private int _currentWaypointIndex;
        [SerializeField] private float _movespeed;
        [SerializeField] private Transform _playerPosition;
        private bool IsReady { get; set; }

        private void FixedUpdate()
        {
            IsReadyCheck();
            if (!IsReady) return;
            var waypoint = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(transform.position, waypoint.position) < 0.01f)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
            else
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, waypoint.position, _movespeed * Time.deltaTime);
            }
        }

        private void IsReadyCheck()
        {
            IsReady = transform.position.z - _playerPosition.transform.position.z <= 13f;
        }
    }
}