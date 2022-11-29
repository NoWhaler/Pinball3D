using System;
using Model;
using Model.Enums;

namespace Gateway
{
    public class BonusWallGateway: IBonusWallGateway
    {
        private readonly BonusWallModel _addition;
        private readonly BonusWallModel _subtraction;
        private readonly BonusWallModel _division;
        private readonly BonusWallModel _multiplication;


        public BonusWallGateway()
        {
            _addition = new BonusWallModel();
            {
                _addition.BonusWallType = BonusWallType.Addition;
            };
            
            _subtraction = new BonusWallModel();
            {
                _subtraction.BonusWallType = BonusWallType.Subtraction;
            };
            
            _division = new BonusWallModel();
            {
                _division.BonusWallType = BonusWallType.Division;
            };
            
            _multiplication = new BonusWallModel();
            {
                _multiplication.BonusWallType = BonusWallType.Multiplication;
            };
        }

        public void SetBonusWallValue(BonusWallType bonusWallType ,int value)
        {
            switch (bonusWallType)
            {
                case BonusWallType.Addition:
                    _addition.Value = value;
                    break;
                case BonusWallType.Subtraction:
                    _subtraction.Value = value;
                    break;
                case BonusWallType.Division:
                    _division.Value = value;
                    break;
                case BonusWallType.Multiplication:
                    _multiplication.Value = value;
                    break;
            }
        }

        public int GetBonusWallValue(BonusWallType bonusWallType)
        {
            return bonusWallType switch
            {
                BonusWallType.Addition => _addition.Value,
                BonusWallType.Subtraction => _subtraction.Value,
                BonusWallType.Division => _division.Value,
                BonusWallType.Multiplication => _multiplication.Value,
                _ => 0
            };
        }

    }
}