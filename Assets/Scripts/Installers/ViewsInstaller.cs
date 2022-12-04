using UI;
using UnityEngine;
using Zenject;

public class ViewsInstaller : MonoInstaller
{
    [SerializeField] private CoinsHolder _coinsHolder;

    public override void InstallBindings()
    {
        Container.Bind<CoinsHolder>().FromInstance(_coinsHolder).AsSingle().NonLazy();
    }
}