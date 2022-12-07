using Model;
using UniRx;

namespace Common.Usecase.Gate
{
    public interface IGateUsecase
    {
        IReadOnlyReactiveProperty<GateModel> GateHealth { get; }
        void SetHealth(int healthValue);
        void ChangeHealth();
    }
}