using UniRx;

namespace Common.Presenter.Gate
{
    public interface IGatePresenter
    {
        IReadOnlyReactiveProperty<int> GateHealth { get; }
        
        void SetHealthPoints(int health);

        void ChangeHealth();
    }
}