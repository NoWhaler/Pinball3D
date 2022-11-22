using Model;
using UnityEngine;
using View;
using Zenject;

namespace Pinball.Presenter
{
    public class BallPresenter : IBallPresenter
    {
        private BallModel _ballModel;
        
        [Inject]
        private IBallView _ballView;
        
        [Inject]
        private IBumperPresenter _bumperPresenter;

        [Inject]
        public BallPresenter(IBallView ballView)
        {
            _ballView = ballView;
            _ballModel = new BallModel();
        }
        

        public void ChangeBallScore()
        {
            _ballModel.Score += _bumperPresenter.ScorePoints;
            _ballView.SetScore(_ballModel.Score);
        }
    }
}