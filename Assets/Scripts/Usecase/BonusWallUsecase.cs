using System.Collections.Generic;
using Gateway;
using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public class BonusWallUsecase: IBonusWallUsecase
    {
        public IReadOnlyReactiveProperty<Dictionary<BonusWallType, BonusWallModel>> Value => _value;
        private ReactiveProperty<Dictionary<BonusWallType, BonusWallModel>> _value;

        private readonly IBonusWallGateway _bonusWallGateway;

        public BonusWallUsecase(IBonusWallGateway bonusWallGateway)
        {
            _bonusWallGateway = bonusWallGateway;
            _value = new ReactiveProperty<Dictionary<BonusWallType, BonusWallModel>>(
                new Dictionary<BonusWallType, BonusWallModel>());   
            
            InitPoints(BonusWallType.Addition);
            InitPoints(BonusWallType.Subtraction);
            InitPoints(BonusWallType.Multiplication);
            InitPoints(BonusWallType.Division);
        }
        
        public void SetValue(BonusWallType bonusWallType)
        {
            var points = _bonusWallGateway.GetBonusWallValue(bonusWallType);

            _bonusWallGateway.SetBonusWallValue(bonusWallType, points);

            var dict = _value.Value;
            dict[bonusWallType].Value = points;
            _value.SetValueAndForceNotify(dict);
        }
        
        private void InitPoints(BonusWallType type)
        {
            var count = new BonusWallModel()
            {
                BonusWallType = type,
                Value =  _bonusWallGateway.GetBonusWallValue(type)
            };
            _value.Value.Add(type, count);
        }
    }
}