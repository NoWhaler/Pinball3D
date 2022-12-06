using UniRx;

namespace DefaultNamespace
{
    public interface IDamageBallPresenter
    {
        IReadOnlyReactiveProperty<int> Damage { get; }

        void SetDamage();
    }
}