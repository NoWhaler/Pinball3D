using System.Collections.Generic;
using Common.Gateway.Bonus;
using Model;
using Model.Enums;
using UniRx;

namespace Common.Usecase.Bonus
{
    public class BonusUsecase: IBonusUsecase
    {   
        public IReadOnlyReactiveProperty<Dictionary<BonusType, BonusModel>> Value => _value;
        private ReactiveProperty<Dictionary<BonusType, BonusModel>> _value;

        public IReadOnlyReactiveProperty<Dictionary<BonusType, BonusModel>> CostValue => _cost;
        private ReactiveProperty<Dictionary<BonusType, BonusModel>> _cost;

        private readonly IBonusGateway _bonusGateway;

        public BonusUsecase(IBonusGateway bonusGateway)
        {
            _bonusGateway = bonusGateway;
            
            _value = new ReactiveProperty<Dictionary<BonusType, BonusModel>>(new Dictionary<BonusType, BonusModel>());
            _cost = new ReactiveProperty<Dictionary<BonusType, BonusModel>>(new Dictionary<BonusType, BonusModel>());
            
            
            InitValue(BonusType.Free);
            InitValue(BonusType.BonusVelocity);
            InitValue(BonusType.BonusTorque);
            InitValue(BonusType.BonusStrength);
        }

        public void SetValue(BonusType bonusType, float value)
        {
            _bonusGateway.SetValue(bonusType, value);
            var dict = _value.Value;
            value = _bonusGateway.GetValue(bonusType);
            dict[bonusType].BonusValue = value;
            _value.SetValueAndForceNotify(dict);
        }

        public void SetCost(BonusType bonusType, int cost)
        {
            _bonusGateway.SetCostValue(bonusType, cost);
            var dict = _cost.Value;
            cost = _bonusGateway.GetCostValue(bonusType);
            dict[bonusType].Cost = cost;
            _cost.SetValueAndForceNotify(dict);
        }

        private void InitValue(BonusType bonusType)
        {
            var model = new BonusModel()
            {
                BonusType = bonusType,
                BonusValue = _bonusGateway.GetValue(bonusType),
            };

            var cost = new BonusModel()
            {
                BonusType = bonusType,
                Cost = _bonusGateway.GetCostValue(bonusType)
            };
            
            _value.Value.Add(bonusType, model);
            _cost.Value.Add(bonusType, cost);
            
        }
    }
}