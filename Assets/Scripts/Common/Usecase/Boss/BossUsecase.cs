using Gateway;
using Model;
using UniRx;
using UnityEngine;

namespace Usecase
{
    public class BossUsecase: IBossUsecase
    {
        public IReadOnlyReactiveProperty<BossModel> Health => _health;
        private ReactiveProperty<BossModel> _health;

        private readonly IBossGateway _bossGateway;

        public BossUsecase(IBossGateway bossGateway)
        {
            _bossGateway = bossGateway;
            _health = new ReactiveProperty<BossModel>(new BossModel());

            InitHealth();
        }

        public void SetHealth(int healthValue)
        {
            _bossGateway.SetBossHealth(healthValue);
            var bossModel = _health.Value;
            healthValue = _bossGateway.GetBossHealth();
            bossModel.HealthPoints = healthValue;
            _health.SetValueAndForceNotify(bossModel);
        }

        public void ChangeHealth()
        {
            var value = _bossGateway.GetBossHealth();
            _bossGateway.SetBossHealth(value);
            var bossModel = _health.Value;
            bossModel.HealthPoints = value;
            _health.SetValueAndForceNotify(bossModel);
        }

        private void InitHealth()
        {
            var healthValue = new BossModel()
            {
                HealthPoints = _bossGateway.GetBossHealth()
            };
            _health.Value = healthValue;
        }
    }
}