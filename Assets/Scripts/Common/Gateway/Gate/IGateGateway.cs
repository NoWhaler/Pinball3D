namespace Common.Gateway.Gate
{
    public interface IGateGateway
    {
        void SetGateHealth(int value);

        int GetGateHealth();

    }
}