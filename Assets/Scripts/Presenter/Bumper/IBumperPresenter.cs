using Model.Enums;
using UniRx;

namespace Pinball.Presenter
{
    public interface IBumperPresenter
    {
        // int ScorePoints { get; }
        IReadOnlyReactiveProperty<int> BumperFive { get; }
        IReadOnlyReactiveProperty<int> BumperTen { get; }
        IReadOnlyReactiveProperty<int> BumperTwenty { get; }
        void SetBumperPoints(BumperType bumperType);
    }
}