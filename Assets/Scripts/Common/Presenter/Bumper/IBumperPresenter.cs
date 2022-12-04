using Model.Enums;
using UniRx;

namespace Pinball.Presenter
{
    public interface IBumperPresenter
    {
        IReadOnlyReactiveProperty<int> BumperFive { get; }
        
        IReadOnlyReactiveProperty<int> BumperTen { get; }
        
        IReadOnlyReactiveProperty<int> BumperTwenty { get; }
        
        IReadOnlyReactiveProperty<int> BumperMinusFive { get; }
        
        IReadOnlyReactiveProperty<int> BumperMinusTen { get; }
        
        IReadOnlyReactiveProperty<int> BumperMinusTwenty { get; }
        
        void SetBumperPoints(BumperType bumperType);
        
    }
}