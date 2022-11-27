using Model;
using UniRx;

namespace Usecase
{
    public interface IBallUsecase
    {
        IReadOnlyReactiveProperty<BallModel> Score { get; }
        void SetScore();
    }
}