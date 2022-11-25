using System;
using CollisionDetection;
using Pinball.Presenter;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace View
{
    public class BallView : MonoBehaviour, IBallView
    {
        private TMP_Text _ballScoreText;
        private Canvas _canvas;
        private Rigidbody _rigidbody;

        [Inject]
        private IBallPresenter _ballPresenter;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _canvas = GetComponentInChildren<Canvas>();
            _ballScoreText = _canvas.GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            _rigidbody.AddForce(new Vector3(0f, 0f, -9.81f), ForceMode.Acceleration);
        }

        public void SetScore(int score)
        {
            _ballScoreText.text = score.ToString();
        }

        private void OnCollisionEnter(Collision col)
        {
            var bumperView = col.collider.GetComponent<BumperView>();
            if (bumperView != null)
            {
                _ballPresenter.ChangeBallScore();
            }
        }
    }
}