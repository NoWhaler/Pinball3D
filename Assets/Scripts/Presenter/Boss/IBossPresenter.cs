using UniRx;

namespace Presenter
{
    public interface IBossPresenter
    {
        
        IReadOnlyReactiveProperty<int> BossHealth { get; }
        // int ScorePoints { get; set; }
        void ChangeHealthPoints();
    }
}