using Model.Enums;
using UniRx;

namespace Common.Presenter.Bonus
{
    public interface IBonusPresenter
    {
        IReadOnlyReactiveProperty<float> FreeBonus { get; }

        IReadOnlyReactiveProperty<float> VelocityBonus { get; }

        IReadOnlyReactiveProperty<float> TorqueBonus { get; }

        IReadOnlyReactiveProperty<float> StrengthBonus { get; }
        
        IReadOnlyReactiveProperty<int> FreeBonusCost { get; }

        IReadOnlyReactiveProperty<int> VelocityBonusCost { get; }

        IReadOnlyReactiveProperty<int> TorqueBonusCost { get; }

        IReadOnlyReactiveProperty<int> StrengthBonusCost { get; }
        
        void SetValue(BonusType bonusType, float value);

        void SetCostValue(BonusType bonusType, int cost);
    }
}