using Pinball.Presenter;
using UnityEngine;
using View;
using Zenject;

public class FlipperPresenter : IFlipperPresenter
{
    private IFlipperView _flipperView;
    
    private Touch _touch;

    public FlipperPresenter(IFlipperView flipperView)
    {
        _flipperView = flipperView;
    }

    public void AddTorque()
    {
        _flipperView.AddTorqueToFlipper();
    }
}


