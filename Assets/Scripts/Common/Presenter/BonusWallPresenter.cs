using System.Collections.Generic;
using Model;
using Model.Enums;
using UniRx;
using UnityEngine;
using Usecase;

namespace Presenter
{
    public class BonusWallPresenter: MonoBehaviour,  IBonusWallPresenter
    {
        public IReadOnlyReactiveProperty<int> BonusWallAddition => _addition;
        private readonly ReactiveProperty<int> _addition = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BonusWallSubtraction => _subtraction;
        private readonly ReactiveProperty<int> _subtraction = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BonusWallDivision => _division;
        private readonly ReactiveProperty<int> _division = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BonusWallMultiplication => _multiplication;
        private readonly ReactiveProperty<int> _multiplication = new ReactiveProperty<int>();

        private IBonusWallUsecase _bonusWallUsecase;
        
        public void Initialize(IBonusWallUsecase bonusWallUsecase)
        {
            _bonusWallUsecase = bonusWallUsecase;
            UpdateCount(_bonusWallUsecase.Value.Value);
        }

        private void UpdateCount(Dictionary<BonusWallType, BonusWallModel> dict)
        {
            foreach (var (key, value) in dict)
            {
                switch (key)
                {
                    case BonusWallType.Addition:
                        _addition.Value = value.Value;
                        break;
                    case BonusWallType.Subtraction:
                        _subtraction.Value = value.Value;
                        break;
                    case BonusWallType.Division:
                        _division.Value = value.Value;
                        break;
                    case BonusWallType.Multiplication:
                        _multiplication.Value = value.Value;
                        break;
                }
            }
        }

        public void SetValueToWall(BonusWallType bonusWallType)
        {
            _bonusWallUsecase.SetValue(bonusWallType);
        }
    }
}