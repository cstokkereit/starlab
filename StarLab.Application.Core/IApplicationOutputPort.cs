using StarLab.Application.Workspace;

namespace StarLab.Application
{
    /// <summary>
    /// Used by a <see cref="UseCaseInteractor{IApplicationOutputPort}"/> to update the application.
    /// </summary>
    public interface IApplicationOutputPort : IOutputPort
    {
        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        void OpenDocument(string id);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided and applies the layout.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        void SetWorkspace(WorkspaceDTO dto);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        /// <param name="documentId">The ID of the document that was modified.</param>
        void UpdateDocument(WorkspaceDTO dto, string documentId);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
