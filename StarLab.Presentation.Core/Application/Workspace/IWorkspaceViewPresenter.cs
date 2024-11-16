namespace StarLab.Application.Workspace
{
    public interface IWorkspaceViewPresenter : IDialogViewPresenter
    {
        void ClearActiveDocument();

        IDockableView CreateView(string id);

        void Initialise(IApplicationController controller, IDockableViewFactory factory);

        void SetActiveDocument(string id);
    }
}
