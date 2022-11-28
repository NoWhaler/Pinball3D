using Model;
using Model.Enums;

namespace Gateway
{
    public class BumperGateway: IBumperGateway
    {
        private readonly BumperModel _bumperModelFive;
        private readonly BumperModel _bumperModelTen;
        private readonly BumperModel _bumperModelTwenty;
        private readonly BumperModel _bumperModelMinusFive;
        private readonly BumperModel _bumperModelMinusTen;
        private readonly BumperModel _bumperModelMinusTwenty;
        

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
            
            _bumperModelMinusFive = new BumperModel();
            {
                _bumperModelMinusFive.Type = BumperType.MinusFive;
            };
            
            _bumperModelMinusTen = new BumperModel();
            {
                _bumperModelMinusTen.Type = BumperType.MinusTen;
            };
            
            _bumperModelMinusTwenty = new BumperModel();
            {
                _bumperModelMinusTwenty.Type = BumperType.MinusTwenty;
            };
        }

        public void SetBumperValue(BumperType bumperType, int value)
        {
            switch (bumperType)
            {
                case BumperType.Five:
                    _bumperModelFive.Points = value;
                    break;
                case BumperType.Ten:
                    _bumperModelTen.Points = value;
                    break;
                case BumperType.Twenty:
                    _bumperModelTwenty.Points = value;
                    break;
                case BumperType.MinusFive:
                    _bumperModelMinusFive.Points = value;
                    break;
                case BumperType.MinusTen:
                    _bumperModelMinusTen.Points = value;
                    break;
                case BumperType.MinusTwenty:
                    _bumperModelMinusTwenty.Points = value;
                    break;
            }
        }

        public int GetBumperValue(BumperType bumperType)
        {
            return bumperType switch
            {
                BumperType.Five => 5,
                BumperType.Ten => 10,
                BumperType.Twenty => 20,
                BumperType.MinusFive => -5,
                BumperType.MinusTen => -10,
                BumperType.MinusTwenty => -20,
                _ => 0
            };
        }
    }
}