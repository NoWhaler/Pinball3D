using Pinball.Presenter;
using Zenject;

public class PresenterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IBallPresenter>().To<BallPresenter>().AsSingle().Lazy();
        Container.Bind<IBumperPresenter>().To<BumperPresenter>().AsTransient().Lazy();
    }
}