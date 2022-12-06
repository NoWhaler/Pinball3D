using Model;
using UniRx;
using UnityEngine;
using Usecase;

namespace DefaultNamespace
{
    public class DamageBallPresenter : MonoBehaviour, IDamageBallPresenter
    {
        private IDamageBallUsecase _damageBallUsecase;

        public IReadOnlyReactiveProperty<int> Damage => _damage;
        private readonly ReactiveProperty<int> _damage = new ReactiveProperty<int>();

        public void Initialize(IDamageBallUsecase damageBallUsecase)
        {
            _damageBallUsecase = damageBallUsecase;
            
            var disposable = _damageBallUsecase.Damage.Subscribe((bossModel) =>
            {
                UpdateDamage(bossModel);
            });
            
            UpdateDamage(_damageBallUsecase.Damage.Value);
        }
        
        private void UpdateDamage(DamageBall damageBall)
        {
            _damage.Value = damageBall.Damage;
        }

        
        public void SetDamage()
        {
            _damageBallUsecase.SetDamage();
        }
    }
}