using Model;
using Model.Enums;
using UnityEngine;
using View;
using Zenject;

namespace Pinball.Presenter
{
    public class BumperPresenter : IBumperPresenter
    {
        private BumperModel _bumperModel;
        
        [Inject]
        private IBumperView _bumperView;
        public int ScorePoints { get; set; }

        [Inject]
        public BumperPresenter(IBumperView bumperView)
        {
            _bumperView = bumperView;
            _bumperModel = new BumperModel();
            ScorePoints = _bumperModel.Points;
        }
        
        public void ChangeBumperPoints()
        {
            _bumperView.SetPoints(_bumperModel.Points);
        }
    }
}