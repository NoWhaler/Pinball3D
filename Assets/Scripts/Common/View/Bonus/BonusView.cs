using System;
using UnityEngine;
using View;

namespace Common.View.Bonus
{
    public class BonusView : MonoBehaviour
    {
        public event Action OnTriggerDetected;
        
        private void OnTriggerEnter(Collider other)
        {
            var ball = other.GetComponent<BallView>();
            if (ball != null)
            {
                OnTriggerDetected?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}