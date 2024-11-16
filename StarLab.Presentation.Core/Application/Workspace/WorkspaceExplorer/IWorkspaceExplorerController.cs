namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerController : IChildViewController
    {
        void Collapse(string key);

        void Rename(string key);

        void OpenDocument(string name);

        void Synchronise();
    }
}
