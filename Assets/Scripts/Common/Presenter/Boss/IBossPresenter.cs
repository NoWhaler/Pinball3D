using UniRx;

namespace Presenter
{
    public interface IBossPresenter
    {
        IReadOnlyReactiveProperty<int> BossHealth { get; }
        
        void SetHealthPoints(int health);

        void ChangeHealth();
    }
}