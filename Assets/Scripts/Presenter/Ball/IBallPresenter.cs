using UniRx;

namespace Pinball.Presenter
{
    public interface IBallPresenter
    {
        IReadOnlyReactiveProperty<int> BallScore { get; }
        void SetBallScore();
        void DealDamageToBoss();
    }
}