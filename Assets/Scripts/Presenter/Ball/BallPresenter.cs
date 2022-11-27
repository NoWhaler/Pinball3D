using Model;
using Presenter;
using UniRx;
using UnityEngine;
using Usecase;
using Zenject;

namespace Pinball.Presenter
{
    public class BallPresenter : MonoBehaviour, IBallPresenter
    {
        // private BallModel _ballModel;

        // [Inject]
        // private IBossPresenter _bossPresenter;

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

        public void SetBallScore()
        {
            _ballUsecase.SetScore();
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