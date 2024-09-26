namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerViewPresenter : IControlViewPresenter
    {
        void DocumentSelected(string id);

        void FolderCollapsed(string key);

        void FolderExpanded(string key);

        void FolderSelected(string key);

        int GetImageIndex(string nodeType, bool expanded, bool selected);

        void OpenDocument(string id);

        void RenameDocument(string id, string name);

        void RenameFolder(string id, string name);

        void ViewActivated();

        void ViewDeactivated();

        void WorkspaceCollapsed();

        void WorkspaceExpanded();

        void WorkspaceSelected();
    }
}
