using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Defines the responses that are available to a workspace use case interactor.
    /// </summary>
    public interface IWorkspaceOutputPort : IOutputPort
    {
        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        void OpenDocument(string id);

        /// <summary>
        /// Removes the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        void RemoveDocument(string id);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        void UpdateWorkspace(WorkspaceDTO dto);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        /// <param name="documentId">The ID of the document that was modified.</param>
        void UpdateWorkspace(WorkspaceDTO dto, string documentId);
    }
}
