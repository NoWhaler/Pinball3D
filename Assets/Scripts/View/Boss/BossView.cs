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

        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _bossHealthText = _canvas.GetComponentInChildren<TMP_Text>();
            // _bossPresenter.ChangeHealthPoints();
        }


        private void Start()
        {
            // _bossPresenter = new BossPresenter(this);
            var reactiveProperty = _bossPresenter.BossHealth;
            
            reactiveProperty.Subscribe((value) =>
            {
                SetHealth(value);
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