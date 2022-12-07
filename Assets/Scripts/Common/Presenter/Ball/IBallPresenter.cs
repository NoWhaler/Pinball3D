using Model.Enums;
using UniRx;

namespace Pinball.Presenter
{
    public interface IBallPresenter
    {
        IReadOnlyReactiveProperty<int> BallScore { get; }
        
        IReadOnlyReactiveProperty<int> BallCombo { get; }
        
        IReadOnlyReactiveProperty<float> BallStrength { get; }

        void SetScoreViaWall(BonusWallType bonusWallType);
        
        void SetBallScoreViaBumper(BumperType bumperType, int value);

        void SetValue(int value);

        void SetComboValue();

        void SetValueViaGate();
        
        void SetScoreViaDamageBall();

        void SetComboToBaseValue();

        void DealDamageToBoss();

        void SetStrengthValue(float value);

        void SetValueViaBonusCost(int cost);
    }
}