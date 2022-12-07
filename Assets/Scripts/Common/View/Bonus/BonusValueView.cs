using System;
using Common.Presenter.Bonus;
using Model.Enums;
using TMPro;
using UniRx;
using UnityEngine;
using View;
using Zenject;

namespace Common.View.Bonus
{
    public class BonusValueView : MonoBehaviour
    {
        private TMP_Text _textValue;
        [SerializeField] private BonusType _bonusType;
        [SerializeField] private float _value;

        private FlipperView[] _flipperView;
        private BallView _ballView;
        
        public event Action OnButtonsClicked;

        [Inject] 
        private IBonusPresenter _bonusPresenter;

        private void OnEnable()
        {
            _textValue = GetComponentInChildren<TMP_Text>();
            _bonusPresenter.SetValue(_bonusType, _value);
            _flipperView = FindObjectsOfType<FlipperView>();
            _ballView = FindObjectOfType<BallView>();
            var reactiveProperty = _bonusType switch
            {
                BonusType.Free => _bonusPresenter.FreeBonus,
                BonusType.BonusTorque => _bonusPresenter.TorqueBonus,
                BonusType.BonusVelocity => _bonusPresenter.VelocityBonus,
                _ => _bonusPresenter.StrengthBonus
            };
            
            reactiveProperty.Subscribe((points) => { SetValue(points); }).AddTo(this);
        }

        public void OnFreeBonusClick()
        {
            OnButtonsClicked?.Invoke();
            // _ballView.Score += (int)_bonusPresenter.FreeBonus.Value;
            _ballView.SetValueViaCost(-(int)_bonusPresenter.FreeBonus.Value);
        }

        public void OnVelocityBonusClick()
        {
            if (_ballView.Score <= _bonusPresenter.VelocityBonusCost.Value) return;
            OnButtonsClicked?.Invoke();
            _ballView.Gravity *= _bonusPresenter.VelocityBonus.Value;
            _ballView.SetValueViaCost(_bonusPresenter.VelocityBonusCost.Value);
        }

        public void OnStrengthBonusClick()
        {
            if (_ballView.Score <= _bonusPresenter.StrengthBonusCost.Value) return;
            OnButtonsClicked?.Invoke();
            _ballView.Strength *= _bonusPresenter.StrengthBonus.Value;
            // _ballView.Score -= _bonusPresenter.StrengthBonusCost.Value;
            _ballView.SetValueViaCost(_bonusPresenter.StrengthBonusCost.Value);
        }

        public void OnTorqueBonusClick()
        {
            foreach (var flipper in _flipperView)
            {
                if (_ballView.Score <= _bonusPresenter.TorqueBonusCost.Value) continue;
                OnButtonsClicked?.Invoke();
                flipper.SpringForce *= _bonusPresenter.TorqueBonus.Value;
                _ballView.SetValueViaCost(_bonusPresenter.TorqueBonusCost.Value);
            }
        }

        private void SetValue(float value)
        {
            _textValue.text = value.ToString();
        }
    }
}