namespace StarLab.Presentation.Docking
{
    public interface IDockableViewPresenter : IPresenter
    {
        string Location { get; set; }
    }
}
