using StarLab.Presentation.Docking;

namespace StarLab.Presentation.Workspaces
{
    public interface IWorkspaceViewPresenter : IFormViewPresenter
    {
        void ClearActiveDocument();

        IDockableView CreateView(string name);

        void Initialise(IApplicationController controller, IDockableViewFactory factory);

        void SetActiveDocument(string name);
    }
}
