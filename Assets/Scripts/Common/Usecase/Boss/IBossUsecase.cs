using Model;
using UniRx;

namespace Usecase
{
    public interface IBossUsecase
    {
        IReadOnlyReactiveProperty<BossModel> Health { get; }
        void SetHealth(int health);
        void ChangeHealth();
    }
}