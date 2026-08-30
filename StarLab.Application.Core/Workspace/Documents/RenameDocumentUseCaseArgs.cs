namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the RenameDocumentUseCase.
    /// </summary>
    public readonly struct RenameDocumentUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RenameDocumentUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="document">The document ID.</param>
        /// <param name="name">The document name.</param>
        public RenameDocumentUseCaseArgs(WorkspaceDTO workspace, string document, string name)
        {
            DocumentID = document;
            Workspace = workspace;
            Name = name;
        }

        public readonly string DocumentID;

        public readonly string Name;

        public readonly WorkspaceDTO Workspace;
    }
}
