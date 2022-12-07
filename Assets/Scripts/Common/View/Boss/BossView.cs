using System;
using AI;
using Interfaces;
using ObjectPooling;
using Presenter;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace View
{
    public class BossView : MonoBehaviour, IBossView, ISetable
    {
        [Inject]
        private IBossPresenter _bossPresenter;
        
        [SerializeField] private int _value;
        [SerializeField] private DamageBallPool _damageBallPool;
        [SerializeField] private Transform _spawnPosition;

        private BossAI _boss;
        
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
            _boss = GetComponent<BossAI>();
        }

        private void Start()
        {
            _bossPresenter.SetHealthPoints(_value);
            var reactiveProperty = _bossPresenter.BossHealth;
            reactiveProperty.Subscribe((value) =>
            {
                SetValue(value);
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
            if (_boss.IsReady)
            {
                _timer += Time.fixedDeltaTime;
                if (!(_timer >= SpawnDelay)) return;
                SpawnDamageBall(_spawnPosition);
                _timer = 0;
            }
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

        public void SetValue(int health)
        {
            _bossHealthText.text = health.ToString();
        }
    }
}