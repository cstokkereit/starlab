using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the AddDocumentUseCase.
    /// </summary>
    public readonly struct AddDocumentUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="document">A <see cref="DocumentDTO"/> that holds the details of the document.</param>
        public AddDocumentUseCaseArgs(WorkspaceDTO workspace, DocumentDTO document)
        {
            Workspace = workspace;
            Document = document;
        }

        public readonly DocumentDTO Document;

        public readonly WorkspaceDTO Workspace;
    }
}
