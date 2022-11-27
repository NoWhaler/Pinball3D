using Gateway;
using Pinball.Presenter;
using Presenter;
using Usecase;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var bumperGateway = new BumperGateway();
        var bumperUsecase = new BumperUsecase(bumperGateway);
        var bumperPresenter = gameObject.AddComponent<BumperPresenter>();
        bumperPresenter.Initialize(bumperUsecase);

        Container.Bind<IBumperGateway>().FromInstance(bumperGateway);
        Container.Bind<IBumperUsecase>().FromInstance(bumperUsecase);
        Container.Bind<IBumperPresenter>().FromInstance(bumperPresenter);


        var ballGateway = new BallGateway();
        var ballUsecase = new BallUsecase(ballGateway);
        var ballPresenter = gameObject.AddComponent<BallPresenter>();
        ballPresenter.Initialize(ballUsecase);

       
        Container.Bind<IBallGateway>().FromInstance(ballGateway);
        Container.Bind<IBallUsecase>().FromInstance(ballUsecase);
        Container.Bind<IBallPresenter>().FromInstance(ballPresenter);

        
        var bossGateway = new BossGateway();
        var bossUsecase = new BossUsecase(bossGateway);
        var bossPresenter = gameObject.AddComponent<BossPresenter>();
        bossPresenter.Initialize(bossUsecase);
        
        Container.Bind<IBossGateway>().FromInstance(bossGateway);
        Container.Bind<IBossUsecase>().FromInstance(bossUsecase);
        Container.Bind<IBossPresenter>().FromInstance(bossPresenter);
        
    }
}