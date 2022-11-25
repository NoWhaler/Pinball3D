using Presenter;
using UnityEngine;

namespace View
{
    public class BossView : MonoBehaviour, IBossView
    {
        private IBossPresenter _bossPresenter;

        private void Start()
        {
            _bossPresenter = new BossPresenter(this);
        }
    }
}