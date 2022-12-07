using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public interface IBallUsecase
    {
        IReadOnlyReactiveProperty<BallModel> Score { get; }
        
        IReadOnlyReactiveProperty<BallModel> Combo { get; }
        
        IReadOnlyReactiveProperty<BallModel> Strength { get; }
        void SetScoreViaBumper(BumperType bumperType, int value);

        void SetScoreViaWall(BonusWallType bonusWallType);

        void SetValueViaGate();
        
        void SetValueViaDamageBall();

        void HitBoss();

        void SetComboToBaseValue();

        void SetComboValue();

        void SetStrength(float value);

        void SetValue(int value);

        void SetValueViaBonusCost(int cost);
    }
}