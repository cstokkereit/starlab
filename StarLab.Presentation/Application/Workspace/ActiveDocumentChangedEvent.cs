namespace StarLab.Application.Workspace
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
