using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

namespace UI
{
    public class UIStateMachine : MonoBehaviour
    {

        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private Canvas _settingsCanvas;
        [SerializeField] private Canvas _gamePlayCanvas;
        [SerializeField] private Canvas _winCanvas;
        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private AudioClip _audioClip;
        
        private BossView _boss;
        
        public bool IsCancelButtonPressed { get; set; }
        public bool IsSettingsButtonPressed { get; set; }
        public bool IsLevelStart { get; set; }
        public bool IsLevelEnded { get; set; }
        public AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }

        public Canvas MenuCanvas => _menuCanvas;

        public Canvas SettingsCanvas => _settingsCanvas;

        public Canvas GamePlayCanvas => _gamePlayCanvas;

        public Canvas WinCanvas => _winCanvas;

        public UIBaseState CurrentState { get; set; }
        private UIStateFactory StateFactory { get; set; }

        private void Awake()
        {
            _boss = FindObjectOfType<BossView>();
            Time.timeScale = 0;
            StateFactory = new UIStateFactory(this);
            CurrentState = StateFactory.Menu();
            CurrentState.EnterState();
            _boss.OnBossDeath += OnLevelEnd;

        }

        private void Update()
        {
            CurrentState.UpdateStates();
            OnLevelStart();
        }

        private void OnLevelStart()
        {
            if (Input.touchCount > 0 || Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = 1;
                IsLevelStart = true;
            }
        }
        
        private void OnLevelEnd()
        {
            IsLevelEnded = true;
            _particleSystem.Play();
        }

        public void OnCancelButtonClick()
        {
            IsCancelButtonPressed = true;
        }

        public void OnSettingsButtonClick()
        {
            IsSettingsButtonPressed = true;
        }

        public void OnNextLevelButtonClick(int sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}