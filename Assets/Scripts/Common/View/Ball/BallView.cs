using System;
using CollisionDetection;
using Common.View.DamageBall;
using Common.View.Gate;
using Interfaces;
using Managers;
using Model.Enums;
using MoreMountains.NiceVibrations;
using Pinball.Presenter;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;

namespace View
{
    public class BallView : MonoBehaviour, IBallView, ISetable
    {
        private TMP_Text _ballScoreText;
        private Canvas _canvas;
        private Rigidbody _rigidbody;

        [SerializeField] private int _score;
        [SerializeField] private float _strength;
        

        public float Strength
        {
            get => _strength;
            set => _strength = value;
        }

        [SerializeField] private ComboView _comboView;
        [SerializeField] private AudioClip _audioClip;

        [Inject]
        private IBallPresenter _ballPresenter;

        public float Gravity { get; set; } = -9.81f;
        public int Score { get => _score; set => _score = value; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _canvas = GetComponentInChildren<Canvas>();
            _ballScoreText = _canvas.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _ballPresenter.SetValue(_score);
            var reactiveProperty = _ballPresenter.BallScore;
            reactiveProperty.Subscribe(SetValue).AddTo(this);
            
            _ballPresenter.SetStrengthValue(Strength);
            var reactivePropertyStrength = _ballPresenter.BallStrength;
            reactivePropertyStrength.Subscribe(UpdateStrengthValue).AddTo(this);
        }

        private void Update()
        {
            _ballPresenter.SetStrengthValue(Strength);
        }

        private void UpdateStrengthValue(float value)
        {
            _strength = value;
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(new Vector3(0f, 0f, Gravity), ForceMode.Acceleration);
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
                // bool test = MMVibrationManager.HapticsSupported();
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

            var gate = collision.collider.GetComponent<GateView>();
            if (gate != null)
            {
                _ballPresenter.SetValueViaGate();
            }
        }

        private void CheckBumperType(BumperView bumperView)
        {
            switch (bumperView.BumperType)
            {
                case BumperType.Five:
                    _ballPresenter.SetBallScoreViaBumper(BumperType.Five, _score);
                    _comboView.IsGettingCombo = true;
                    _comboView.ComboTimer();
                    break;
                case BumperType.Ten:
                    _ballPresenter.SetBallScoreViaBumper(BumperType.Ten, _score);
                    _comboView.IsGettingCombo = true;
                    _comboView.ComboTimer();
                    break;
                case BumperType.Twenty:
                    _ballPresenter.SetBallScoreViaBumper(BumperType.Twenty, _score);
                    _comboView.IsGettingCombo = true;
                    _comboView.ComboTimer();
                    break;
                case BumperType.MinusFive:
                    _ballPresenter.SetBallScoreViaBumper(BumperType.MinusFive, _score);
                    break;
                case BumperType.MinusTen:
                    _ballPresenter.SetBallScoreViaBumper(BumperType.MinusTen, _score);
                    break;
                case BumperType.MinusTwenty:
                    _ballPresenter.SetBallScoreViaBumper(BumperType.MinusTwenty, _score);
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
        
        public void SetValue(int score)
        {
            _ballScoreText.text = score.ToString();
        }

        public void SetValueViaCost(int cost)
        {
            _ballPresenter.SetValueViaBonusCost(cost);
        }
    }
}