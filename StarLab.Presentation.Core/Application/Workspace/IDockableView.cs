namespace StarLab.Application.Workspace
{
    public interface IDockableView : IView
    {
        string DefaultLocation { get; set; }
    }
}
