using System;
using ObjectPooling;
using UnityEngine;
using UnityEngine.UIElements;
using View;
using Zenject;

namespace DefaultNamespace
{
    public class GateSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;

        private GatePool _gatePool;
        private BoxCollider _collider;

        private void Start()
        {
            _collider = GetComponent<BoxCollider>();
            _gatePool = FindObjectOfType<GatePool>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var ballView = other.GetComponent<BallView>();
            if (ballView == null) return;
            _collider.enabled = false;
            SpawnDamageBall(_spawnPosition);
        }
        
        private void SpawnDamageBall(Transform spawnPosition)
        {
            var objectPool = _gatePool.Get();

            if (objectPool == null) return;
            objectPool.transform.position = spawnPosition.position;
            objectPool.transform.rotation = spawnPosition.rotation;
            objectPool.gameObject.SetActive(true);
        }
    }
}