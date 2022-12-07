using Common.Presenter.Bonus;
using Interfaces;
using Model.Enums;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Common.View.Bonus
{
    public class BonusCostView : MonoBehaviour, ISetable
    {
        private TMP_Text _textValue;
        [SerializeField] private BonusType _bonusType;
        [SerializeField] private int _cost;

        [Inject] 
        private IBonusPresenter _bonusPresenter;
        private void OnEnable()
        {
            _textValue = GetComponentInChildren<TMP_Text>();
            _bonusPresenter.SetCostValue(_bonusType, _cost);
            var reactiveProperty = _bonusType switch
            {
                BonusType.Free => _bonusPresenter.FreeBonusCost,
                BonusType.BonusVelocity => _bonusPresenter.VelocityBonusCost,
                BonusType.BonusTorque => _bonusPresenter.TorqueBonusCost,
                _ => _bonusPresenter.StrengthBonusCost
            };

            reactiveProperty.Subscribe((points) => { SetValue(points); }).AddTo(this);
        }
        
        public void SetValue(int value)
        {
            _textValue.text = value.ToString();
        }
    }
}