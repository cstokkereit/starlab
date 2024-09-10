namespace StarLab.Presentation.Workspaces.WorkspaceExplorer
{
    public interface IWorkspaceExplorerController : IController
    {
        void CollapseAll();

        void Synchronise();
    }
}
