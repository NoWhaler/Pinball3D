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

        private BossView _boss;
        
        public bool IsCancelButtonPressed { get; set; }
        public bool IsSettingsButtonPressed { get; set; }
        public bool IsScreenTouched { get; set; }
        public bool IsLevelEnded { get; set; }

        public Canvas MenuCanvas
        {
            get => _menuCanvas;
        }

        public Canvas SettingsCanvas
        {
            get => _settingsCanvas;
        }

        public Canvas GamePlayCanvas
        {
            get => _gamePlayCanvas;
        }

        public Canvas WinCanvas
        {
            get => _winCanvas;
        }
        
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
            OnScreenTouch();
        }

        private void OnScreenTouch()
        {
            if (Input.touchCount > 0)
            {
                Time.timeScale = 1;
                IsScreenTouched = true;
            }
        }
        
        private void OnLevelEnd()
        {
            IsLevelEnded = true;
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