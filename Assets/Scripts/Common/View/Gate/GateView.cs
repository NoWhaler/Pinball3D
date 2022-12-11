using System;
using System.Collections.Generic;
using Common.Presenter.Gate;
using Interfaces;
using ObjectPooling;
using TMPro;
using UniRx;
using UnityEngine;
using View;
using Zenject;

namespace Common.View.Gate
{
    public class GateView : MonoBehaviour, ISetable
    {
        [Inject] 
        private IGatePresenter _gatePresenter;
        
        [SerializeField] private int _value;

        private TMP_Text _gateHealthText;
        private Canvas _canvas;

        private GatePool _gatePool;

        private void OnEnable()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _gateHealthText = _canvas.GetComponentInChildren<TMP_Text>();
            _gatePool = FindObjectOfType<GatePool>();
            _gatePresenter.SetHealthPoints(_value);
            var reactiveProperty = _gatePresenter.GateHealth;
            reactiveProperty.Subscribe((value) =>
            {
                SetValue(value);
                if (value <= 0)
                {
                    _gatePool.ReturnToPool(this);
                }
            }).AddTo(this);
        }

        private void OnDisable()
        {
            _value += 250;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var ballView = collision.collider.GetComponent<BallView>();
            if (ballView != null)
            {
                _gatePresenter.ChangeHealth();
            }
        }
        
        public void SetValue(int health)
        {
            _gateHealthText.text = health.ToString();
        }
    }
}