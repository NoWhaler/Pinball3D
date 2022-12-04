using Model.Enums;

namespace Gateway
{
    public interface IBumperGateway
    {
        void SetBumperValue(BumperType bumperType, int value);
        
        int GetBumperValue(BumperType bumperType);
    }
}