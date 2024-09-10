using StarLab.Presentation.Model;

namespace StarLab.Presentation.Workspaces
{
    public class WorkspaceChangedEvent
    {
        public WorkspaceChangedEvent(IWorkspace workspace)
        {
            Workspace = workspace;
        }

        public IWorkspace Workspace { get; }
    }
}
