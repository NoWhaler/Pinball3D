using System;
using Model;
using ObjectPooling;
using Presenter;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace View
{
    public class BossView : MonoBehaviour, IBossView
    {
        [Inject]
        private IBossPresenter _bossPresenter;
        
        [SerializeField] private int _value;
        [SerializeField] private DamageBallPool _damageBallPool;
        [SerializeField] private Transform _spawnPosition;
        
        private TMP_Text _bossHealthText;
        private Canvas _canvas;

        private const float SpawnDelay = 10f;
        private float _timer;

        public event Action OnBossDeath;

        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _bossHealthText = _canvas.GetComponentInChildren<TMP_Text>();
            _damageBallPool = FindObjectOfType<DamageBallPool>();
        }

        private void Start()
        {
            SpawnDamageBall(_spawnPosition);
            
            _bossPresenter.SetHealthPoints(_value);
            var reactiveProperty = _bossPresenter.BossHealth;
            reactiveProperty.Subscribe((value) =>
            {
                SetHealth(value);
                if (value <= 0)
                {
                    OnBossDeath?.Invoke();
                    gameObject.SetActive(false);
                }
            }).AddTo(this);
        }

        private void FixedUpdate()
        {
            SpawnOnTime();
        }

        private void SpawnOnTime()
        {
            _timer += Time.fixedDeltaTime;
            if (!(_timer >= SpawnDelay)) return;
            SpawnDamageBall(_spawnPosition);
            _timer = 0;
        }
        
        private void SpawnDamageBall(Transform spawnPosition)
        {
            var objectPool = _damageBallPool.Get();

            if (objectPool == null) return;
            objectPool.transform.position = spawnPosition.position;
            objectPool.transform.rotation = spawnPosition.rotation;
            objectPool.gameObject.SetActive(true);
        }
        
        
        private void OnCollisionEnter(Collision collision)
        {
            var ballView = collision.collider.GetComponent<BallView>();
            if (ballView != null)
            {
                _bossPresenter.ChangeHealth();
            }
        }

        private void SetHealth(int health)
        {
            _bossHealthText.text = health.ToString();
        }
    }
}