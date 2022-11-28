using Model.Enums;
using UniRx;

namespace Pinball.Presenter
{
    public interface IBumperPresenter
    {
        IReadOnlyReactiveProperty<int> BumperFive { get; }
        
        IReadOnlyReactiveProperty<int> BumperTen { get; }
        
        IReadOnlyReactiveProperty<int> BumperTwenty { get; }
        
        void SetBumperPoints(BumperType bumperType);

        // void AddPoints(BumperType bumperType);
    }
}