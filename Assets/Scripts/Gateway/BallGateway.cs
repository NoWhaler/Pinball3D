using Model;

namespace Gateway
{
    public class BallGateway: IBallGateway
    {
        private readonly BallModel _ballModel;

        public BallGateway()
        {
            _ballModel = new BallModel();
        }

        public void SetBallValue(int value)
        {
            _ballModel.Score = value;
        }

        public int GetBallValue()
        {
            return _ballModel.Score;
        }
    }
}