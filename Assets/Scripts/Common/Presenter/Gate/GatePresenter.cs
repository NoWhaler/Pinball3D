using System.Collections.Generic;
using Common.Usecase.Gate;
using Model;
using UniRx;
using UnityEngine;

namespace Common.Presenter.Gate
{
    public class GatePresenter : MonoBehaviour, IGatePresenter
    {
        private IGateUsecase  _gateUsecase;
        public IReadOnlyReactiveProperty<int> GateHealth => _gateHealth;
        private readonly ReactiveProperty<int> _gateHealth = new ReactiveProperty<int>();

        public void Initialize(IGateUsecase gateUsecase)
        {
            _gateUsecase = gateUsecase;
            
            var disposable = _gateUsecase.GateHealth.Subscribe((gateModel) =>
            {
                UpdateHealth(gateModel);
            });
            UpdateHealth(_gateUsecase.GateHealth.Value);
            Debug.Log("Set presenter");
        }
        
        private void UpdateHealth(GateModel gateModel)
        {
            _gateHealth.Value = gateModel.GateHealth;
        }

        public void SetHealthPoints(int health)
        {
            _gateUsecase.SetHealth(health);
        }

        public void ChangeHealth()
        {
            _gateUsecase.ChangeHealth();
        }
    }
}