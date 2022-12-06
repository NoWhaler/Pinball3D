using UniRx;
using Model;

namespace Usecase
{
    public interface IDamageBallUsecase
    {
        IReadOnlyReactiveProperty<DamageBall> Damage { get; }
        
        void SetDamage();
    }
}