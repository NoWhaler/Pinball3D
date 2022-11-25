using Model;
using View;

namespace Presenter
{
    public class BossPresenter: IBossPresenter
    {
        private IBossView _bossView;
        private BossModel _bossModel;

        public BossPresenter(IBossView bossView)
        {
            _bossModel = new BossModel();
            _bossView = bossView;
        }
    }
}