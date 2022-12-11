using System.Collections.Generic;
using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public interface IBonusWallUsecase
    {
        IReadOnlyReactiveProperty<Dictionary<BonusWallType, BonusWallModel>> Value { get; }

        void SetValue(BonusWallType bonusWallType, int value);
    }
}