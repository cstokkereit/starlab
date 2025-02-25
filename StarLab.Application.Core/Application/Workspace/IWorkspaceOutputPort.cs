using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Defines the responses that are available to a workspace use case interactor.
    /// </summary>
    public interface IWorkspaceOutputPort : IOutputPort
    {
        /// <summary>
        /// Deletes the specified documents.
        /// </summary>
        /// <param name="dtos">An <see cref="IEnumerable{DocumentDTO}"/> that specifies the documents to be deleted.</param>
        void DeleteDocuments(IEnumerable<DocumentDTO> dtos);

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The ID of the document to be opened.</param>
        void OpenDocument(string id);

        /// <summary>
        /// Updates the state of the document represented by the <see cref="DocumentDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="DocumentDTO"/> that contains the updated document state.</param>
        void UpdateDocument(DocumentDTO dto);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
