using Model.Enums;
using UniRx;

namespace Presenter
{
    public interface IBonusWallPresenter
    {
        public IReadOnlyReactiveProperty<int> BonusWallAddition { get; }
        
        public IReadOnlyReactiveProperty<int> BonusWallSubtraction { get; }
        
        public IReadOnlyReactiveProperty<int> BonusWallDivision { get; }
        
        public IReadOnlyReactiveProperty<int> BonusWallMultiplication { get; }
        
        void SetValueToWall(BonusWallType bonusWallType);
    }
}