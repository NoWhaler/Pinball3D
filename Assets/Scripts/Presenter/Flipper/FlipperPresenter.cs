using Pinball.Presenter;
using View;

public class FlipperPresenter : IFlipperPresenter
{
    private IFlipperView _flipperView;

    public FlipperPresenter(IFlipperView flipperView)
    {
        _flipperView = flipperView;
    }

    public void AddTorque()
    {
        _flipperView.AddSpringToFlipper();
    }
}


