namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerController : IController
    {
        void CollapseAll();

        void Rename(string key);

        void OpenDocument(string name);

        void Synchronise();
    }
}
