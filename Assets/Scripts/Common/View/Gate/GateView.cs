using System;
using System.Collections.Generic;
using Common.Presenter.Gate;
using Interfaces;
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

        private void Awake()
        {
            _canvas = GetComponentInChildren<Canvas>();
            _gateHealthText = _canvas.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _gatePresenter.SetHealthPoints(_value);
            var reactiveProperty = _gatePresenter.GateHealth;
            reactiveProperty.Subscribe((value) =>
            {
                SetValue(value);
                if (value <= 0)
                {
                    gameObject.SetActive(false);
                }
            }).AddTo(this);
            Debug.Log("I created View");
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