using StarLab.Presentation.Model;

namespace StarLab.Application.Workspace
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
