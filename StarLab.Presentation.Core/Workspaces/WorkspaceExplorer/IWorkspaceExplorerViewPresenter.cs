namespace StarLab.Presentation.Workspaces.WorkspaceExplorer
{
    public interface IWorkspaceExplorerViewPresenter : IControlViewPresenter
    {
        string GetImageKey(string nodeType, bool expanded);
    }
}
