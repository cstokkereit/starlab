namespace StarLab.Application.Workspace
{
    public interface IWorkspaceViewPresenter : IDialogViewPresenter
    {
        void ClearActiveDocument();

        IDockableView CreateView(string id);

        void SetActiveDocument(string id);
    }
}
