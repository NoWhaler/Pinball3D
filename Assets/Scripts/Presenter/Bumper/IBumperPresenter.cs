namespace Pinball.Presenter
{
    public interface IBumperPresenter
    {
        int ScorePoints { get; }
        void ChangeBumperPoints();
    }
}