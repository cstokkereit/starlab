using StarLab.Presentation;

namespace StarLab.Application.Workspace
{
    public interface IDockableViewPresenter : IPresenter, IDialogController, IViewController
    {
        string Location { get; set; }
    }
}
