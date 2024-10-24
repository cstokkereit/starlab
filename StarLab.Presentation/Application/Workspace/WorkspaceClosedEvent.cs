namespace StarLab.Application.Workspace
{
    public class WorkspaceClosedEvent
    {
        public WorkspaceClosedEvent(IWorkspace workspace)
        {
            Workspace = workspace;
        }

        public IWorkspace Workspace { get; }
    }
}
