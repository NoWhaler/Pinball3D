using System;
using Interfaces;
using Pinball.Presenter;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace View
{
    public class ComboView : MonoBehaviour, ISetable
    {
        private TMP_Text _comboValueText;

        [Inject]
        private IBallPresenter _ballPresenter;

        private const float WaitTime = 2f;
        
        private float Timer { get; set; }
        
        public bool IsGettingCombo { get; set; }
        
        private void OnEnable()
        {
            _comboValueText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            var reactiveProperty = _ballPresenter.BallCombo;
            
            reactiveProperty.Subscribe((value) => {
                SetValue(value);
            }).AddTo(this);
        }

        private void FixedUpdate()
        {
            Timer += Time.fixedDeltaTime;
            CheckForCombo();
        }

        private void CheckForCombo()
        {
            if (Timer < WaitTime) return;
            IsGettingCombo = false;
           _ballPresenter.SetComboToBaseValue();
        }

        public void ComboTimer()
        {
            if (!IsGettingCombo) return;
            _ballPresenter.SetComboValue();
            Timer = 0;
        }

        public void SetValue(int value)
        {
            _comboValueText.text = value.ToString();
        }
    }
}