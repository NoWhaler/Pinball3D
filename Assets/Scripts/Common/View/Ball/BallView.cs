using System;
using CollisionDetection;
using Common.View.DamageBall;
using Managers;
using Model;
using Model.Enums;
using MoreMountains.NiceVibrations;
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

        [SerializeField] private ComboView _comboView;
        [SerializeField] private AudioClip _audioClip;

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
        }

        private void SetScore(int score)
        {
            _ballScoreText.text = score.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            var bonusWall = other.GetComponent<BonusWallView>();
            if (bonusWall == null) return;
            
            CheckBonusWallType(bonusWall);
            other.gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var bumperView = collision.collider.GetComponent<BumperView>();
            if (bumperView != null)
            {
                CheckBumperType(bumperView);
            }
            
            var bossView = collision.collider.GetComponent<BossView>();
            if (bossView != null)
            {
                _ballPresenter.DealDamageToBoss();
                bool test = MMVibrationManager.HapticsSupported();
            }

            var trampoline = collision.collider.GetComponent<Trampoline>();
            if (trampoline != null)
            {
                AudioManager.Instance.PlayAudioClip(_audioClip);
            }

            var damageBall = collision.collider.GetComponent<DamageBallView>();
            if (damageBall != null)
            {
                _ballPresenter.SetScoreViaDamageBall();
                damageBall.gameObject.SetActive(false);
            }
        }

        private void CheckBumperType(BumperView bumperView)
        {
            switch (bumperView.BumperType)
            {
                case BumperType.Five:
                    _ballPresenter.SetBallScore(BumperType.Five);
                    _comboView.IsGettingCombo = true;
                    _comboView.ComboTimer();
                    break;
                case BumperType.Ten:
                    _ballPresenter.SetBallScore(BumperType.Ten);
                    _comboView.IsGettingCombo = true;
                    _comboView.ComboTimer();
                    break;
                case BumperType.Twenty:
                    _ballPresenter.SetBallScore(BumperType.Twenty);
                    _comboView.IsGettingCombo = true;
                    _comboView.ComboTimer();
                    break;
                case BumperType.MinusFive:
                    _ballPresenter.SetBallScore(BumperType.MinusFive);
                    break;
                case BumperType.MinusTen:
                    _ballPresenter.SetBallScore(BumperType.MinusTen);
                    break;
                case BumperType.MinusTwenty:
                    _ballPresenter.SetBallScore(BumperType.MinusTwenty);
                    break;
            }
        }
        
        private void CheckBonusWallType(BonusWallView bonusWallView)
        {
            switch (bonusWallView.BonusWallType)
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
        }
    }
}