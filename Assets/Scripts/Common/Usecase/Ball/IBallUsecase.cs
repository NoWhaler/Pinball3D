using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public interface IBallUsecase
    {
        IReadOnlyReactiveProperty<BallModel> Score { get; }
        
        IReadOnlyReactiveProperty<BallModel> Combo { get; }
        void SetScoreViaBumper(BumperType bumperType);

        void SetScoreViaWall(BonusWallType bonusWallType);

        void HitBoss();

        void SetComboToBaseValue();

        void SetComboValue();
    }
}