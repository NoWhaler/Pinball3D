using Common.Gateway.Gate;
using Model;
using UniRx;
using UnityEngine;

namespace Common.Usecase.Gate
{
    public class GateUsecase: IGateUsecase
    {
        public IReadOnlyReactiveProperty<GateModel> GateHealth => _gateHealth;
        private ReactiveProperty<GateModel> _gateHealth = new ReactiveProperty<GateModel>();

        public IReactiveCollection<GateModel> GateModels => _gateModels;
        private ReactiveCollection<GateModel> _gateModels;

        private readonly IGateGateway  _gateGateway;

        public GateUsecase(IGateGateway gateGateway)
        {
             _gateGateway = gateGateway;
             _gateHealth = new ReactiveProperty<GateModel>(new GateModel());
             InitHealth();
             Debug.Log("I created Usecase");
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
            _gateGateway.GetGateHealth();
            var bossModel = _gateHealth.Value;
            bossModel.GateHealth = value;
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