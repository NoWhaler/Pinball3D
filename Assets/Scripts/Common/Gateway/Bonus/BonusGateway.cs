using System;
using Model;
using Model.Enums;

namespace Common.Gateway.Bonus
{
    public class BonusGateway: IBonusGateway
    {
        private readonly BonusModel _freeModel;
        private readonly BonusModel _velocityModel;
        private readonly BonusModel _torqueModel;
        private readonly BonusModel _strengthModel;


        public BonusGateway()
        {
            _freeModel = new BonusModel();
            {
                _freeModel.BonusType = BonusType.Free;
            };
            
            _velocityModel = new BonusModel();
            {
                _velocityModel.BonusType = BonusType.BonusVelocity;
            };
            
            _torqueModel = new BonusModel();
            {
                _torqueModel.BonusType = BonusType.BonusTorque;
            };
            
            _strengthModel = new BonusModel();
            {
                _strengthModel.BonusType = BonusType.BonusStrength;
            };
        }

        public void SetValue(BonusType bonusType, float value)
        {
            switch (bonusType)
            {
                case BonusType.Free:
                    _freeModel.BonusValue = value;
                    break;
                case BonusType.BonusVelocity:
                    _velocityModel.BonusValue = value;
                    break;
                case BonusType.BonusTorque:
                    _torqueModel.BonusValue = value;
                    break;
                case BonusType.BonusStrength:
                    _strengthModel.BonusValue = value;
                    break;
            }
        }

        public void SetCostValue(BonusType bonusType, int cost)
        {
            switch (bonusType)
            {
                case BonusType.Free:
                    _freeModel.Cost = cost;
                    break;
                case BonusType.BonusVelocity:
                    _velocityModel.Cost = cost;
                    break;
                case BonusType.BonusTorque:
                    _torqueModel.Cost = cost;
                    break;
                case BonusType.BonusStrength:
                    _strengthModel.Cost = cost;
                    break;
            }
        }

        public int GetCostValue(BonusType bonusType)
        {
            return bonusType switch
            {
                BonusType.Free => _freeModel.Cost,
                BonusType.BonusVelocity => _velocityModel.Cost,
                BonusType.BonusTorque => _torqueModel.Cost,
                BonusType.BonusStrength => _strengthModel.Cost,
                _ => 0
            };
        }

        public float GetValue(BonusType bonusType)
        {
            return bonusType switch
            {
                BonusType.Free => _freeModel.BonusValue,
                BonusType.BonusVelocity => _velocityModel.BonusValue,
                BonusType.BonusTorque => _torqueModel.BonusValue,
                BonusType.BonusStrength => _strengthModel.BonusValue,
                _ => 0
            };
        }
    }
}