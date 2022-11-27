using Gateway;
using Model;
using UniRx;

namespace Usecase
{
    public class BossUsecase: IBossUsecase
    {
        public IReadOnlyReactiveProperty<BossModel> Health => _health;
        private ReactiveProperty<BossModel> _health;

        private readonly IBossGateway _bossGateway;
        private readonly IBallGateway _ballGateway;

        public BossUsecase(IBossGateway bossGateway, IBallGateway ballGateway)
        {
            _bossGateway = bossGateway;
            _ballGateway = ballGateway;
            _health = new ReactiveProperty<BossModel>(new BossModel());

            InitHealth();
        }

        public void SetHealth()
        {
            var score = _bossGateway.GetBossHealth();
            var newValue = score;

            _bossGateway.SetBossHealth(newValue);

            var bossModel = _health.Value;
            bossModel.HealthPoints = newValue;
            _health.SetValueAndForceNotify(bossModel);
        }

        private void InitHealth()
        {
            var count = new BossModel()
            {
                HealthPoints = _bossGateway.GetBossHealth()
            };
            _health.Value = count;
        }
    }
}