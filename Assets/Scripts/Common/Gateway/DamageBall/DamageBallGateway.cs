
namespace Common.Gateway.DamageBall
{
    public class DamageBallGateway: IDamageBallGateway
    {

        private readonly Model.DamageBall _damageBall;

        public DamageBallGateway()
        {
            _damageBall = new Model.DamageBall();
        }

        public void SetDamage(int damage)
        {
            _damageBall.Damage = damage;
        }

        public int GetDamageValue()
        {
            return _damageBall.Damage;
        }
    }
}