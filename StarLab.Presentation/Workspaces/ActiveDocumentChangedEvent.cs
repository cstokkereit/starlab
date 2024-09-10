using StarLab.Presentation.Model;

namespace StarLab.Presentation.Workspaces
{
    public class ActiveDocumentChangedEvent
    {
        public ActiveDocumentChangedEvent(IWorkspace workspace)
        {
            Workspace = workspace;
        }

        public IWorkspace Workspace { get; }
    }
}
