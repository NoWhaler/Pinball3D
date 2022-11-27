using Gateway;
using Model;
using UniRx;

namespace Usecase
{
    public class BallUsecase: IBallUsecase
    {
        public IReadOnlyReactiveProperty<BallModel> Score => _score;
        private ReactiveProperty<BallModel> _score;

        private readonly IBallGateway _ballGateway;

        public BallUsecase(IBallGateway ballGateway)
        {
            _ballGateway = ballGateway;
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