using Model;
using UnityEngine;

namespace Common.Gateway.Gate
{
    public class GateGateway: IGateGateway
    {
        private readonly GateModel _gateModelOne;

        public GateGateway()
        {
            _gateModelOne= new GateModel();
            Debug.Log("I created Model");
        }

        public void  SetGateHealth(int value)
        {
            _gateModelOne.GateHealth = value;
        }

        public int GetGateHealth()
        {
            return _gateModelOne.GateHealth;
        }

    }
}