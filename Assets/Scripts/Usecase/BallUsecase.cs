using Gateway;
using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public class BallUsecase: IBallUsecase
    {
        public IReadOnlyReactiveProperty<BallModel> Score => _score;
        private readonly ReactiveProperty<BallModel> _score;

        private readonly IBallGateway _ballGateway;
        private IBossGateway _bossGateway;

        public BallUsecase(IBallGateway ballGateway, IBossGateway bossGateway)
        {
            _ballGateway = ballGateway;
            _bossGateway = bossGateway;
            _score = new ReactiveProperty<BallModel>(new BallModel());
            InitScore();
        }

        public void SetScore()
        {
            var score = _ballGateway.GetBallValue();
            var newValue = score + 10;
            _ballGateway.SetBallValue(newValue);

            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
        }

        public void HitBoss()
        {
            var score = _ballGateway.GetBallValue();
            var ballGetDamageValue = _bossGateway.GetBossHealth() + (score - _bossGateway.GetBossHealth());
            var bossGetDamageValue = score + (_bossGateway.GetBossHealth() - score);

            if (score > _bossGateway.GetBossHealth())
            {
                _bossGateway.SetBossHealth(0);
                score -= bossGetDamageValue;
            }
            else
            {
                
                _bossGateway.SetBossHealth(_bossGateway.GetBossHealth() - score);
                score = 0;
            }
            
            var newValue = score;

            _ballGateway.SetBallValue(newValue);
            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
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
        
        private void InitScore()
        {
            var count = new BallModel()
            {
                Score = _ballGateway.GetBallValue()
            };
            _score.Value = count;
        }
    }
}