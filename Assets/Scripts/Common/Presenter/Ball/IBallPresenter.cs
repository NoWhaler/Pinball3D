using Model.Enums;
using UniRx;

namespace Pinball.Presenter
{
    public interface IBallPresenter
    {
        IReadOnlyReactiveProperty<int> BallScore { get; }
        
        IReadOnlyReactiveProperty<int> BallCombo { get; }

        void SetScoreViaWall(BonusWallType bonusWallType);
        
        void SetBallScore(BumperType bumperType);

        void SetComboValue();
        
        void SetScoreViaDamageBall();

        void SetComboToBaseValue();

        void DealDamageToBoss();
    }
}