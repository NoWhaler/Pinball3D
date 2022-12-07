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

        public void SetComboValue(int comboValue)
        {
            _ballModel.Combo = comboValue;
        }

        public int GetComboValue()
        {
            return _ballModel.Combo;
        }

        public int GetBallValue()
        {
            return _ballModel.Score;
        }

        public void SetStrengthValue(float strength)
        {
            _ballModel.DamageStrength = strength;
        }

        public float GetBallStrength()
        {
            return _ballModel.DamageStrength;
        }
    }
}