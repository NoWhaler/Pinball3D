using UniRx;

namespace Presenter
{
    public interface IBossPresenter
    {
        IReadOnlyReactiveProperty<int> BossHealth { get; }
        
        void ChangeHealthPoints();
    }
}