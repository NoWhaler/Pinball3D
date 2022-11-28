using System;
using System.Collections.Generic;
using Gateway;
using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public class BumperUsecase: IBumperUsecase
    {
        public IReadOnlyReactiveProperty<Dictionary<BumperType, BumperModel>> Points => _points;
        private ReactiveProperty<Dictionary<BumperType, BumperModel>> _points;

        private readonly IBumperGateway _bumperGateway;
        
        public BumperUsecase(IBumperGateway bumperGateway)
        {
            _bumperGateway = bumperGateway;
            _points = new ReactiveProperty<Dictionary<BumperType, BumperModel>>(
                new Dictionary<BumperType, BumperModel>());
            
            InitPoints(BumperType.Five);
            InitPoints(BumperType.Ten);
            InitPoints(BumperType.Twenty);
            InitPoints(BumperType.MinusFive);
            InitPoints(BumperType.MinusTen);
            InitPoints(BumperType.MinusTwenty);
        }

        public void SetPoints(BumperType bumperType)
        {
            var points = _bumperGateway.GetBumperValue(bumperType);

            _bumperGateway.SetBumperValue(bumperType, points);

            var dict = _points.Value;
            dict[bumperType].Points = points;
            _points.SetValueAndForceNotify(dict);
        }

        private void InitPoints(BumperType type)
        {
            var count = new BumperModel()
            {
                Type = type,
                Points = _bumperGateway.GetBumperValue(type)
            };
            _points.Value.Add(type, count);
        }
    }
}