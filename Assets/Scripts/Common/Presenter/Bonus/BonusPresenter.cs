using System;
using System.Collections.Generic;
using Common.Usecase.Bonus;
using Model;
using Model.Enums;
using UniRx;
using UnityEngine;

namespace Common.Presenter.Bonus
{
    public class BonusPresenter : MonoBehaviour, IBonusPresenter
    {
        public IReadOnlyReactiveProperty<float> FreeBonus => _free;
        private readonly ReactiveProperty<float> _free = new ReactiveProperty<float>();
        
        public IReadOnlyReactiveProperty<float> VelocityBonus => _velocity;
        private readonly ReactiveProperty<float> _velocity = new ReactiveProperty<float>();
        
        public IReadOnlyReactiveProperty<float> TorqueBonus => _torque;
        private readonly ReactiveProperty<float> _torque = new ReactiveProperty<float>();
        
        public IReadOnlyReactiveProperty<float> StrengthBonus => _strength;
        private readonly ReactiveProperty<float> _strength = new ReactiveProperty<float>();
        
        public IReadOnlyReactiveProperty<int> FreeBonusCost => _freeCost;
        private readonly ReactiveProperty<int> _freeCost = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> VelocityBonusCost => _velocityCost;
        private readonly ReactiveProperty<int> _velocityCost = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> TorqueBonusCost => _torqueCost;
        private readonly ReactiveProperty<int> _torqueCost = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> StrengthBonusCost => _strengthCost;
        private readonly ReactiveProperty<int> _strengthCost = new ReactiveProperty<int>();


        private IBonusUsecase _bonusUsecase;

        public void Initialize(IBonusUsecase bonusUsecase)
        {
            _bonusUsecase = bonusUsecase;
            var disposable = _bonusUsecase.Value.Subscribe((bossModel) =>
            {
                UpdateValue(bossModel);
            });
            UpdateValue(_bonusUsecase.Value.Value);
            
            var disposableCost = _bonusUsecase.CostValue.Subscribe((bossModel) =>
            {
                UpdateCostValue(bossModel);
            });
            UpdateCostValue(_bonusUsecase.CostValue.Value);
        }

        private void UpdateValue(Dictionary<BonusType, BonusModel> dictionary)
        {
            foreach (var (key, value) in dictionary)
            {
                switch (key)
                {
                    case BonusType.Free:
                        _free.Value = value.BonusValue;
                        break;
                    case BonusType.BonusVelocity:
                        _torque.Value = value.BonusValue;
                        break;
                    case BonusType.BonusTorque:
                        _velocity.Value = value.BonusValue;
                        break;
                    case BonusType.BonusStrength:
                        _strength.Value = value.BonusValue;
                        break;
                }
            }
        }

        private void UpdateCostValue(Dictionary<BonusType, BonusModel> dictionary)
        {
            foreach (var (key, value) in dictionary)
            {
                switch (key)
                {
                    case BonusType.Free:
                        _freeCost.Value = value.Cost;
                        break;
                    case BonusType.BonusVelocity:
                        _velocityCost.Value = value.Cost;
                        break;
                    case BonusType.BonusTorque:
                        _torqueCost.Value = value.Cost;
                        break;
                    case BonusType.BonusStrength:
                        _strengthCost.Value = value.Cost;
                        break;
                }
            }
        }

        public void SetValue(BonusType bonusType, float value)
        {
            _bonusUsecase.SetValue(bonusType, value);
        }

        public void SetCostValue(BonusType bonusType, int cost)
        {
            _bonusUsecase.SetCost(bonusType, cost);
        }
    }
}