namespace StarLab.Application.Workspace.Documents
{
    public interface IAddDocumentOutputPort : IOutputPort
    {
        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        void OpenDocument(string id);

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
