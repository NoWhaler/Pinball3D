using System;
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

        private void FixedUpdate()
        {
            _rigidbody.AddForce(new Vector3(0f, 0f, -9.81f), ForceMode.Acceleration);
            Debug.Log(_rigidbody.velocity.z);
        }

        private void SetScore(int score)
        {
            _ballScoreText.text = score.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            var bonusWall = other.GetComponent<BonusWallView>();
            if (bonusWall != null)
            {
                switch (bonusWall.BonusWallType)
                {
                    case BonusWallType.Addition:
                        _ballPresenter.SetScoreViaWall(BonusWallType.Addition);
                        break;
                    case BonusWallType.Subtraction:
                        _ballPresenter.SetScoreViaWall(BonusWallType.Subtraction);
                        break;
                    case BonusWallType.Division:
                        _ballPresenter.SetScoreViaWall(BonusWallType.Division);
                        break;
                    case BonusWallType.Multiplication:
                        _ballPresenter.SetScoreViaWall(BonusWallType.Multiplication);
                        break;
                }
                other.gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var bumperView = collision.collider.GetComponent<BumperView>();
            if (bumperView != null)
            {
                switch (bumperView.BumperType)
                {
                    case BumperType.Five:
                        _ballPresenter.SetBallScore(BumperType.Five);
                        break;
                    case BumperType.Ten:
                        _ballPresenter.SetBallScore(BumperType.Ten);
                        break;
                    case BumperType.Twenty:
                        _ballPresenter.SetBallScore(BumperType.Twenty);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            var bossView = collision.collider.GetComponent<BossView>();
            if (bossView != null)
            {
                _ballPresenter.DealDamageToBoss();
            }
            
        }
    }
}