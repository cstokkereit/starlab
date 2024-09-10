namespace StarLab.Presentation.Docking
{
    public interface IDockableView : IView
    {
        string DefaultLocation { get; }
    }
}
