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
        
        public IReadOnlyReactiveProperty<float> BallStrength => _ballStrength;
        private ReactiveProperty<float> _ballStrength = new ReactiveProperty<float>();
        
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
            
            var disposableStrength = _ballUsecase.Strength.Subscribe((ballModel) =>
            {
                UpdateStrength(ballModel);
            });
            UpdateStrength(_ballUsecase.Strength.Value);
        }

        public void SetBallScoreViaBumper(BumperType bumperType, int value)
        {
            _ballUsecase.SetScoreViaBumper(bumperType, value);
        }

        public void SetValue(int value)
        {
            _ballUsecase.SetValue(value);
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

        public void SetValueViaGate()
        {
            _ballUsecase.SetValueViaGate();
        }

        public void SetStrengthValue(float value)
        {
            _ballUsecase.SetStrength(value);
        }

        public void SetValueViaBonusCost(int cost)
        {
            _ballUsecase.SetValueViaBonusCost(cost);
        }
        
        private void UpdateScore(BallModel ballModel)
        {
            _ballScore.Value = ballModel.Score;
        }

        private void UpdateCombo(BallModel ballModel)
        {
            _ballCombo.Value = ballModel.Combo;
        }

        private void UpdateStrength(BallModel ballModel)
        {
            _ballStrength.Value = ballModel.DamageStrength;
        }
    }
}