using UnityEngine;

namespace View
{
    public class MovableGates : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _waitTime;

        [SerializeField] private Transform _basePosition;
        [SerializeField] private Transform _finalPosition;

        private float Timer { get; set; }
        private bool IsReverse { get; set; }
        private bool IsWaiting { get; set; }

        private void FixedUpdate()
        {
            CheckReverse();
            if (IsWaiting)
            {
                Timer += Time.fixedDeltaTime;
                if (Timer < _waitTime)
                    return;
                Timer = 0;
                IsWaiting = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, !IsReverse ? _finalPosition.position :
                _basePosition.position, _speed * Time.deltaTime);
        }

        private void CheckReverse()
        {
            if (transform.position == _finalPosition.position)
            {
                IsWaiting = true;
                IsReverse = true;
            }
            else if (transform.position == _basePosition.position)
            {
                IsWaiting = true;
                IsReverse = false;
            }
        }
    }
    
}