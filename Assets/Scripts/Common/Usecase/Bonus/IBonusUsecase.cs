using System.Collections.Generic;
using Model;
using Model.Enums;
using UniRx;

namespace Common.Usecase.Bonus
{
    public interface IBonusUsecase
    {

        IReadOnlyReactiveProperty<Dictionary<BonusType, BonusModel>> Value { get; }
        
        IReadOnlyReactiveProperty<Dictionary<BonusType, BonusModel>> CostValue { get; }

        void SetCost(BonusType bonusType, int cost);
    
        void SetValue(BonusType bonusType, float value);
    }
}