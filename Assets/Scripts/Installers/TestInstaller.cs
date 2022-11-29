using Gateway;
using Pinball.Presenter;
using Presenter;
using Usecase;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var ballGateway = new BallGateway();
        var bossGateway = new BossGateway();
        var bumperGateway = new BumperGateway();
        var bonusWallGateway = new BonusWallGateway();
        
        var bumperUsecase = new BumperUsecase(bumperGateway);
        var bumperPresenter = gameObject.AddComponent<BumperPresenter>();
        bumperPresenter.Initialize(bumperUsecase);

        Container.Bind<IBumperGateway>().FromInstance(bumperGateway).NonLazy();
        Container.Bind<IBumperUsecase>().FromInstance(bumperUsecase).NonLazy();
        Container.Bind<IBumperPresenter>().FromInstance(bumperPresenter).NonLazy();


        var ballUsecase = new BallUsecase(ballGateway, bossGateway, bumperGateway, bonusWallGateway);
        var ballPresenter = gameObject.AddComponent<BallPresenter>();
        ballPresenter.Initialize(ballUsecase);

        Container.Bind<IBallGateway>().FromInstance(ballGateway).NonLazy();
        Container.Bind<IBallUsecase>().FromInstance(ballUsecase).NonLazy();
        Container.Bind<IBallPresenter>().FromInstance(ballPresenter).NonLazy();


        var bossUsecase = new BossUsecase(bossGateway, ballGateway);
        var bossPresenter = gameObject.AddComponent<BossPresenter>();
        bossPresenter.Initialize(bossUsecase);
        
        Container.Bind<IBossGateway>().FromInstance(bossGateway).NonLazy();
        Container.Bind<IBossUsecase>().FromInstance(bossUsecase).NonLazy();
        Container.Bind<IBossPresenter>().FromInstance(bossPresenter).NonLazy();

        var bonusWallUsecase = new BonusWallUsecase(bonusWallGateway);
        var bonusWallPresenter = gameObject.AddComponent<BonusWallPresenter>();
        bonusWallPresenter.Initialize(bonusWallUsecase);
        
        Container.Bind<IBonusWallGateway>().FromInstance(bonusWallGateway).NonLazy();
        Container.Bind<IBonusWallUsecase>().FromInstance(bonusWallUsecase).NonLazy();
        Container.Bind<IBonusWallPresenter>().FromInstance(bonusWallPresenter).NonLazy();

    }
}