using Model;


namespace Common.Gateway.Gate
{
    public class GateGateway: IGateGateway
    {
        private readonly GateModel _gateModel;

        public GateGateway()
        {
            _gateModel= new GateModel();
            // Debug.Log("I created Model");
        }

        public void  SetGateHealth(int value)
        {
            _gateModel.GateHealth = value;
        }

        public int GetGateHealth()
        {
            return _gateModel.GateHealth;
        }

    }
}