namespace StarLab.Application.Workspace
{
    public interface IDockableViewPresenter : IPresenter
    {
        string Location { get; set; }
    }
}
