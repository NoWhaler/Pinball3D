using Model;
using Model.Enums;

namespace Gateway
{
    public class BumperGateway: IBumperGateway
    {
        private readonly BumperModel _bumperModelFive;
        private readonly BumperModel _bumperModelTen;
        private readonly BumperModel _bumperModelTwenty;

        public BumperGateway()
        {
            _bumperModelFive = new BumperModel();
            {
                _bumperModelFive.Type = BumperType.Five;
            };
            
            _bumperModelTen = new BumperModel();
            {
                _bumperModelTen.Type = BumperType.Ten;
            };
            
            _bumperModelTwenty = new BumperModel();
            {
                _bumperModelTwenty.Type = BumperType.Twenty;
            };
        }

        public void SetBumperValue(BumperType bumperType, int value)
        {
            if (bumperType == BumperType.Five)
            {
                _bumperModelFive.Points = value;
            }
            else if (bumperType == BumperType.Ten)
            {
                _bumperModelTen.Points = value;
            }
            else if (bumperType == BumperType.Twenty)
            {
                _bumperModelTwenty.Points = value;
            }
        }

        public int GetBumperValue(BumperType bumperType)
        {
            if (bumperType == BumperType.Five)
            {
                return 5;
            }
            if (bumperType == BumperType.Ten)
            {
                return 10;
            }
            if (bumperType == BumperType.Twenty)
            {
                return 20;
            }
            else
            {
                return 0;
            }
        }
    }
}