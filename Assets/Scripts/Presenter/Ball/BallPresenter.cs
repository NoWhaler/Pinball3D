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
            // _bossPresenter.ChangeHealthPoints();
            _ballUsecase.SetScore();
        }
        
        private void UpdateScore(BallModel ballModel)
        {
            _ballScore.Value = ballModel.Score;
        }
        
        // private void CountDamageValue()
        // {
        //     var value = _bossPresenter.ScorePoints + (_ballModel.Score - _bossPresenter.ScorePoints);
        //     var value2 = _ballModel.Score + (_bossPresenter.ScorePoints - _ballModel.Score);
        //     
        //     if (_bossPresenter.ScorePoints < _ballModel.Score)
        //     {
        //         _ballModel.Score -= _bossPresenter.ScorePoints;
        //         _bossPresenter.ScorePoints -= value2;
        //     }
        //     else
        //     {
        //         _ballModel.Score -= _ballModel.Score;
        //         _bossPresenter.ScorePoints -= value;
        //     }
        // }
    }
}