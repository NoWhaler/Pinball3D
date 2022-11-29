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
        
        public void Initialize(IBallUsecase usecase)
        {
            _ballUsecase = usecase;
            var disposable = _ballUsecase.Score.Subscribe((ballModel) =>
            {
                UpdateScore(ballModel);
            });
            UpdateScore(_ballUsecase.Score.Value);
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
        
        private void UpdateScore(BallModel ballModel)
        {
            _ballScore.Value = ballModel.Score;
        }
    }
}