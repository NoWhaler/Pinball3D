using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public interface IBallUsecase
    {
        IReadOnlyReactiveProperty<BallModel> Score { get; }
        void SetScore(BumperType bumperType);

        void HitBoss();
    }
}