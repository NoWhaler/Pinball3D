using System;
using DefaultNamespace;
using ObjectPooling;
using UnityEngine;
using View;
using Zenject;

namespace Common.View.DamageBall
{
    public class DamageBallView : MonoBehaviour
    {
        [Inject] 
        private IDamageBallPresenter _damageBallPresenter;
        
        [SerializeField] private DamageBallPool _damageBallPool;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _damageBallPresenter.SetDamage();
        }
        
        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _damageBallPool = FindObjectOfType<DamageBallPool>();
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(new Vector3(0f, 0f, -9.81f), ForceMode.Acceleration);
        }

        private void OnTriggerEnter(Collider other)
        {
            var ball = other.GetComponent<BallView>();
            if (ball != null)
            {
                _damageBallPool.ReturnToPool(this);
            }
        }
    }
}