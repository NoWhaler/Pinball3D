using System.Collections.Generic;
using Model;
using Model.Enums;
using UniRx;

namespace Usecase
{
    public interface IBumperUsecase
    {
        IReadOnlyReactiveProperty<Dictionary<BumperType, BumperModel>> Points { get; }
        void SetPoints(BumperType bumperType);

        // void AddPointsToBall(BumperType bumperType);
    }
}