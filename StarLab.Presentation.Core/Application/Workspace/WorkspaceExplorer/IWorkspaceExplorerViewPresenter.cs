namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerViewPresenter : IControlViewPresenter
    {
        void DocumentSelected(string id);

        void FolderCollapsed(string key);

        void FolderExpanded(string key);

        void FolderSelected(string key);

        int GetImageIndex(string nodeType, bool expanded, bool selected);

        void Initialise(IApplicationController controller, IFormController parentController);

        void OpenDocument(string id);

        void RenameDocument(string id, string name);

        void RenameFolder(string id, string name);

        void ShowErrorMessage(string message);

        void ViewActivated();

        void ViewDeactivated();

        void WorkspaceCollapsed();

        void WorkspaceExpanded();

        void WorkspaceSelected();
    }
}
