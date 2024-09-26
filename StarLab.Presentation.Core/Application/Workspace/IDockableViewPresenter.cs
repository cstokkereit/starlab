using StarLab.Presentation;

namespace StarLab.Application.Workspace
{
    public interface IDockableViewPresenter : IPresenter, IViewController
    {
        string Location { get; set; }
    }
}
