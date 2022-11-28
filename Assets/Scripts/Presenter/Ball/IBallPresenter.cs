using Model.Enums;
using UniRx;

namespace Pinball.Presenter
{
    public interface IBallPresenter
    {
        IReadOnlyReactiveProperty<int> BallScore { get; }
        void SetBallScore(BumperType bumperType);
        void DealDamageToBoss();
    }
}