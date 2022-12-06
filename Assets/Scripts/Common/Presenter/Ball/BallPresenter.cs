using Model;
using Model.Enums;
using UniRx;
using UnityEngine;
using Usecase;

namespace Pinball.Presenter
{
    public class BallPresenter : MonoBehaviour, IBallPresenter
    {
        private IBallUsecase _ballUsecase;

        public IReadOnlyReactiveProperty<int> BallScore => _ballScore;
        private readonly ReactiveProperty<int> _ballScore = new ReactiveProperty<int>();
        
        public IReadOnlyReactiveProperty<int> BallCombo => _ballCombo;
        private ReactiveProperty<int> _ballCombo = new ReactiveProperty<int>();
        
        public void Initialize(IBallUsecase usecase)
        {
            _ballUsecase = usecase;
            var disposable = _ballUsecase.Score.Subscribe((ballModel) =>
            {
                UpdateScore(ballModel);
            });
            UpdateScore(_ballUsecase.Score.Value);
            
            var disposableValue = _ballUsecase.Combo.Subscribe((ballModel) =>
            {
                UpdateCombo(ballModel);
            });
            UpdateCombo(_ballUsecase.Combo.Value);
        }

        public void SetBallScore(BumperType bumperType)
        {
            _ballUsecase.SetScoreViaBumper(bumperType);
        }

        public void SetScoreViaWall(BonusWallType bonusWallType)
        {
            _ballUsecase.SetScoreViaWall(bonusWallType);
        }

        public void DealDamageToBoss()
        {
            _ballUsecase.HitBoss();
        }

        public void SetComboValue()
        {
            _ballUsecase.SetComboValue();
        }

        public void SetComboToBaseValue()
        {
            _ballUsecase.SetComboToBaseValue();
        }

        public void SetScoreViaDamageBall()
        {
            _ballUsecase.SetValueViaDamageBall();
        }
        
        private void UpdateScore(BallModel ballModel)
        {
            _ballScore.Value = ballModel.Score;
        }

        private void UpdateCombo(BallModel ballModel)
        {
            _ballCombo.Value = ballModel.Combo;
        }
    }
}