using Model.Enums;

namespace Common.Gateway.Bonus
{
    public interface IBonusGateway
    {
        void SetValue(BonusType bonusType, float value);
        void SetCostValue(BonusType bonusType, int cost);

        float GetValue(BonusType bonusType);
        int GetCostValue(BonusType bonusType);
    }
}