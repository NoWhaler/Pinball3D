using Model.Enums;

namespace Gateway
{
    public interface IBonusWallGateway
    { 
        void SetBonusWallValue(BonusWallType bonusWallType, int value);
        int GetBonusWallValue(BonusWallType bonusWallType);
    }
}