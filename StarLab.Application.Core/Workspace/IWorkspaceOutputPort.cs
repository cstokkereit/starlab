namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Used by a <see cref="UseCaseInteractor{TOutputPort}"/> to update the workspace.
    /// </summary>
    public interface IWorkspaceOutputPort : IOutputPort
    {
        /// <summary>
        /// Clears the clipboard.
        /// </summary>
        void ClearClipboard();

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        void OpenDocument(string id);

        /// <summary>
        /// Renames the specified folder.
        /// </summary>
        /// <param name="path">The folder path.</param>
        void RenameFolder(string path);

        /// <summary>
        /// Updates the contents of the clipboard.
        /// </summary>
        /// <param name="key">The key that identifies the target of the current clipboard operation.</param>
        void UpdateClipboard(string key);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        /// <param name="id">The ID of the document that was modified.</param>
        void UpdateDocument(WorkspaceDTO dto, string id);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
