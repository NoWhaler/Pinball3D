using Common.Gateway.DamageBall;
using Gateway;
using Model;
using UniRx;

namespace Usecase
{
    public class DamageBallUsecase: IDamageBallUsecase
    {
        public IReadOnlyReactiveProperty<DamageBall> Damage => _damage;

        private ReactiveProperty<DamageBall> _damage = new ReactiveProperty<DamageBall>();

        private readonly IDamageBallGateway _damageBallGateway;

        public DamageBallUsecase(IDamageBallGateway damageBallGateway)
        {
            _damageBallGateway = damageBallGateway;
            _damage = new ReactiveProperty<DamageBall>();

            InitDamage();
        }

        public void SetDamage()
        {
            var damage = _damageBallGateway.GetDamageValue();
            // var ballScore = _ballGateway.GetBallValue();
            // var newValue = ballScore - damage;
            // _ballGateway.SetBallValue(newValue);
            _damageBallGateway.SetDamage(damage);
            var bossModel = _damage.Value;
            bossModel.Damage = damage;
            _damage.SetValueAndForceNotify(bossModel);
        }

        private void InitDamage()
        {
            var damageValue = new DamageBall()
            {
                Damage = _damageBallGateway.GetDamageValue()
            };
            _damage.Value = damageValue;
        }
    }
}