using UI;
using UnityEngine;
using View;
using Zenject;

public class ViewsInstaller : MonoInstaller
{
    [SerializeField] private BumperView _bumperView;
    [SerializeField] private BallView _ballView;

    [SerializeField] private CoinsHolder _coinsHolder;

    public override void InstallBindings()
    {
        Container.Bind<IBumperView>().To<BumperView>().FromComponentOn(_bumperView.gameObject).AsTransient().NonLazy();
        Container.Bind<IBallView>().To<BallView>().FromComponentOn(_ballView.gameObject).AsSingle().NonLazy();
        Container.Bind<CoinsHolder>().FromInstance(_coinsHolder).AsSingle().NonLazy();
    }
}