using System;
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
        
        private TMP_Text _bossHealthText;
        private Canvas _canvas;

        public event Action OnBossDeath;

        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _bossHealthText = _canvas.GetComponentInChildren<TMP_Text>();
        }


        private void Start()
        {
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
            // SetHealth(1000);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var ballView = collision.collider.GetComponent<BallView>();
            if (ballView != null)
            {
                _bossPresenter.ChangeHealthPoints();
            }
        }

        private void SetHealth(int health)
        {
            _bossHealthText.text = health.ToString();
        }
    }
}