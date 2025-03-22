namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents a use case that adds a document to the workspace.
    /// </summary>
    public interface IAddDocumentUseCase
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="dtoDocument">A <see cref="DocumentDTO"/> that defines the document being added.</param>
        void Execute(WorkspaceDTO dtoWorkspace, DocumentDTO dtoDocument);
    }
}
