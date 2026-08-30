namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the DeleteDocumentUseCase.
    /// </summary>
    public readonly struct DeleteDocumentUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DeleteDocumentUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="document">The document ID.</param>
        public DeleteDocumentUseCaseArgs(WorkspaceDTO workspace, string document)
        {
            DocumentID = document;
            Workspace = workspace;
        }

        public readonly string DocumentID;

        public readonly WorkspaceDTO Workspace;
    }
}
