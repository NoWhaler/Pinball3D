using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public interface IBallUsecase
    {
        IReadOnlyReactiveProperty<BallModel> Score { get; }
        void SetScoreViaBumper(BumperType bumperType);

        void SetScoreViaWall(BonusWallType bonusWallType);

        void HitBoss();
    }
}