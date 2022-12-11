using Common.Gateway.Bonus;
using Common.Gateway.DamageBall;
using Common.Gateway.Gate;
using Common.Presenter.Bonus;
using Common.Presenter.Gate;
using Common.Usecase.Bonus;
using Common.Usecase.Gate;
using DefaultNamespace;
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
        var damageBallgateway = new DamageBallGateway();
        var gateGateway = new GateGateway();
        var bonusGateway = new BonusGateway();
        
        var bumperUsecase = new BumperUsecase(bumperGateway);
        var bumperPresenter = gameObject.AddComponent<BumperPresenter>();
        bumperPresenter.Initialize(bumperUsecase);

        Container.Bind<IBumperGateway>().FromInstance(bumperGateway).AsTransient().NonLazy();
        Container.Bind<IBumperUsecase>().FromInstance(bumperUsecase).AsTransient().NonLazy();
        Container.Bind<IBumperPresenter>().FromInstance(bumperPresenter).AsTransient().NonLazy();


        var ballUsecase = new BallUsecase(ballGateway, bossGateway, bumperGateway, bonusWallGateway, damageBallgateway, gateGateway, bonusGateway);
        var ballPresenter = gameObject.AddComponent<BallPresenter>();
        ballPresenter.Initialize(ballUsecase);

        Container.Bind<IBallGateway>().FromInstance(ballGateway).AsTransient().NonLazy();
        Container.Bind<IBallUsecase>().FromInstance(ballUsecase).AsTransient().NonLazy();
        Container.Bind<IBallPresenter>().FromInstance(ballPresenter).AsTransient().NonLazy();


        var bossUsecase = new BossUsecase(bossGateway);
        var bossPresenter = gameObject.AddComponent<BossPresenter>();
        bossPresenter.Initialize(bossUsecase);
        
        Container.Bind<IBossGateway>().FromInstance(bossGateway).AsTransient().NonLazy();
        Container.Bind<IBossUsecase>().FromInstance(bossUsecase).AsTransient().NonLazy();
        Container.Bind<IBossPresenter>().FromInstance(bossPresenter).AsTransient().NonLazy();

        
        var bonusWallUsecase = new BonusWallUsecase(bonusWallGateway);
        var bonusWallPresenter = gameObject.AddComponent<BonusWallPresenter>();
        bonusWallPresenter.Initialize(bonusWallUsecase);
        
        Container.Bind<IBonusWallGateway>().FromInstance(bonusWallGateway).AsTransient().NonLazy();
        Container.Bind<IBonusWallUsecase>().FromInstance(bonusWallUsecase).AsTransient().NonLazy();
        Container.Bind<IBonusWallPresenter>().FromInstance(bonusWallPresenter).AsTransient().NonLazy();

        
        var damageBallUsecase = new DamageBallUsecase(damageBallgateway);
        var damageBallPresenter = gameObject.AddComponent<DamageBallPresenter>();
        damageBallPresenter.Initialize(damageBallUsecase);
        
        Container.Bind<IDamageBallGateway>().FromInstance(damageBallgateway).AsTransient().NonLazy();
        Container.Bind<IDamageBallUsecase>().FromInstance(damageBallUsecase).AsTransient().NonLazy();
        Container.Bind<IDamageBallPresenter>().FromInstance(damageBallPresenter).AsTransient().NonLazy();

        
        var gateUsecase = new GateUsecase(gateGateway, ballGateway);
        var gatePresenter = gameObject.AddComponent<GatePresenter>();
        gatePresenter.Initialize(gateUsecase);

        Container.Bind<IGateGateway>().FromInstance(gateGateway).AsTransient().NonLazy();
        Container.Bind<IGateUsecase>().FromInstance(gateUsecase).AsTransient().NonLazy();
        Container.Bind<IGatePresenter>().FromInstance(gatePresenter).AsTransient().NonLazy();


        var bonusUsecase = new BonusUsecase(bonusGateway);
        var bonusPresenter = gameObject.AddComponent<BonusPresenter>();
        bonusPresenter.Initialize(bonusUsecase);
        
        Container.Bind<IBonusGateway>().FromInstance(bonusGateway).AsTransient().NonLazy();
        Container.Bind<IBonusUsecase>().FromInstance(bonusUsecase).AsTransient().NonLazy();
        Container.Bind<IBonusPresenter>().FromInstance(bonusPresenter).AsTransient().NonLazy();
    }
}