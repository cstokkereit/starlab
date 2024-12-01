namespace StarLab.Application.Workspace.Documents
{
    public readonly struct AddDocumentInteractionContext : IInteractionContext
    { 
        public AddDocumentInteractionContext(IWorkspace workspace, string path, DocumentType type)
        {
            Workspace = workspace;
            Path = path;
            Type = type;
        }

        public AddDocumentInteractionContext(IWorkspace workspace, string path)
        {
            Workspace = workspace;
            Path = path;
        }

        public string Path { get; }

        public DocumentType Type { get; }

        public IWorkspace Workspace { get; }
    }
}
