using Model;
using UniRx;
using UnityEngine;
using Usecase;

namespace Presenter
{
    public class BossPresenter: MonoBehaviour, IBossPresenter
    {
        private IBossUsecase _bossUsecase;
        
        public IReadOnlyReactiveProperty<int> BossHealth => _bossHealth;
        private readonly ReactiveProperty<int> _bossHealth = new ReactiveProperty<int>();

        public void Initialize(IBossUsecase bossUsecase)
        {
            _bossUsecase = bossUsecase;
            var disposable = _bossUsecase.Health.Subscribe((bossModel) =>
            {
                UpdateHealth(bossModel);
            });
            UpdateHealth(_bossUsecase.Health.Value);
        }
        
        private void UpdateHealth(BossModel bossModel)
        {
            _bossHealth.Value = bossModel.HealthPoints;
        }

        public void SetHealthPoints(int health)
        {
            _bossUsecase.SetHealth(health);
        }

        public void ChangeHealth()
        {
            _bossUsecase.ChangeHealth();
        }
    }
}