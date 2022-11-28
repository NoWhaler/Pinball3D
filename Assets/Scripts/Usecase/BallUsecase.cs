using System;
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
        private readonly IBossGateway _bossGateway;
        private readonly IBumperGateway _bumperGateway;

        public BallUsecase(IBallGateway ballGateway, IBossGateway bossGateway, IBumperGateway bumperGateway)
        {
            _ballGateway = ballGateway;
            _bossGateway = bossGateway;
            _bumperGateway = bumperGateway;
            _score = new ReactiveProperty<BallModel>(new BallModel());
            InitScore();
        }

        public void SetScore(BumperType bumperType)
        {
            var score = _ballGateway.GetBallValue();
            
            switch (bumperType)
            {
                case BumperType.Five:
                    score += _bumperGateway.GetBumperValue(BumperType.Five);
                    break;
                case BumperType.Ten:
                    score += _bumperGateway.GetBumperValue(BumperType.Ten);
                    break;
                case BumperType.Twenty:
                    score += _bumperGateway.GetBumperValue(BumperType.Twenty);
                    break;
                case BumperType.MinusFive:
                    score -= _bumperGateway.GetBumperValue(BumperType.Five);
                    break;
                case BumperType.MinusTen:
                    score -= _bumperGateway.GetBumperValue(BumperType.Ten);
                    break;
                case BumperType.MinusTwenty:
                    score -= _bumperGateway.GetBumperValue(BumperType.Twenty);
                    break;
            }
            
            var newValue = score;
            
            _ballGateway.SetBallValue(newValue);

            var ballModel = _score.Value;
            ballModel.Score = newValue;
            _score.SetValueAndForceNotify(ballModel);
        }

        public void HitBoss()
        {
            var score = _ballGateway.GetBallValue();
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