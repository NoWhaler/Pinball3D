﻿using AI;
using Model.Enums;
using Pinball.Presenter;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;

namespace View
{
    public class BallView : MonoBehaviour, IBallView
    {
        private TMP_Text _ballScoreText;
        private Canvas _canvas;
        private Rigidbody _rigidbody;
        private BumperType _bumperType;

        private int _score;

        [Inject]
        private IBallPresenter _ballPresenter;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _canvas = GetComponentInChildren<Canvas>();
            _ballScoreText = _canvas.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            var reactiveProperty = _ballPresenter.BallScore;
            
            reactiveProperty.Subscribe((value) => {
                SetScore(value);
            }).AddTo(this);
            SetScore(0);
        }

        private void Update()
        {
            _rigidbody.AddForce(new Vector3(0f, 0f, -9.81f), ForceMode.Acceleration);
        }

        private void SetScore(int score)
        {
            _ballScoreText.text = score.ToString();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var bumperView = collision.collider.GetComponent<BumperView>();
            if (bumperView != null)
            {
                _ballPresenter.SetBallScore();
            }
            
            var bossView = collision.collider.GetComponent<BossView>();
            if (bossView != null)
            {
                _ballPresenter.DealDamageToBoss();
            }
            
        }
    }
}