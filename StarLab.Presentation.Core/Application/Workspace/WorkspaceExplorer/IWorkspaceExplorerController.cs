namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerController : IController
    {
        void CollapseAll();

        void Rename(string document);

        void OpenDocument(string name);

        void Synchronise();
    }
}
