using Common.Gateway.Gate;
using Gateway;
using Model;
using UniRx;
using UnityEngine;

namespace Common.Usecase.Gate
{
    public class GateUsecase: IGateUsecase
    {
        public IReadOnlyReactiveProperty<GateModel> GateHealth => _gateHealth;
        private ReactiveProperty<GateModel> _gateHealth = new ReactiveProperty<GateModel>();

        private readonly IGateGateway _gateGateway;
        private IBallGateway _ballGateway;

        public GateUsecase(IGateGateway gateGateway, IBallGateway ballGateway)
        {
             _gateGateway = gateGateway;
             _ballGateway = ballGateway;
             _gateHealth = new ReactiveProperty<GateModel>(new GateModel());
             InitHealth();
             // Debug.Log("I created Usecase");
        }

        public void SetHealth(int healthValue)
        {
            _gateGateway.SetGateHealth(healthValue);
            var bossModel = _gateHealth.Value;
            healthValue = _gateGateway.GetGateHealth();
            bossModel.GateHealth = healthValue;
            _gateHealth.SetValueAndForceNotify(bossModel);
        }

        public void ChangeHealth()
        {
            var value = _gateGateway.GetGateHealth();
            if (value > _ballGateway.GetBallValue())
            {
                value -= _ballGateway.GetBallValue();
                _ballGateway.SetBallValue(0);
            }
            else
            {
                _ballGateway.SetBallValue(_ballGateway.GetBallValue() - value);
                value = 0;
            }
            var newValue = value;
            _gateGateway.SetGateHealth(newValue);
            var bossModel = _gateHealth.Value;
            bossModel.GateHealth = newValue;
            _gateHealth.SetValueAndForceNotify(bossModel);
        }

        private void InitHealth()
        {
            var healthValue = new GateModel()
            {
                GateHealth = _gateGateway.GetGateHealth()
            };
            _gateHealth.Value = healthValue;
        }
    }
}