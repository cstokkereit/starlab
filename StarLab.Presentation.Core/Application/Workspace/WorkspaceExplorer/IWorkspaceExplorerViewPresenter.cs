namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerViewPresenter : IChildViewPresenter
    {
        void DocumentSelected(string id);

        void FolderCollapsed(string key);

        void FolderExpanded(string key);

        void FolderSelected(string key);

        int GetImageIndex(string nodeType, bool expanded, bool selected);

        void OpenDocument(string id);

        void ProjectCollapsed(string key);

        void ProjectExpanded(string key);

        void ProjectSelected(string key);

        void RenameDocument(string id, string name);

        void RenameFolder(string id, string name);

        void RenameWorkspace(string name);

        void ShowErrorMessage(string message);

        void ViewActivated();

        void ViewDeactivated();

        void WorkspaceCollapsed();

        void WorkspaceExpanded();

        void WorkspaceSelected();
    }
}
