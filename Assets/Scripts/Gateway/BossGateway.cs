using Model;

namespace Gateway
{
    public class BossGateway: IBossGateway
    {
        private readonly BossModel _bossModel;

        public BossGateway()
        {
            _bossModel = new BossModel();
        }

        public void SetBossHealth(int value)
        {
            _bossModel.HealthPoints = value;
        }

        public int GetBossHealth()
        {
            return _bossModel.HealthPoints;
        }
    }
}