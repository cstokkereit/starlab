namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerController : IController
    {
        void Collapse(string key);

        void Rename(string key);

        void OpenDocument(string name);

        void Synchronise();
    }
}
