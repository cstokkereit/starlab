using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IWorkspaceOutputPort : IOutputPort
    {
        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The ID of the document to be opened.</param>
        void OpenDocument(string id);

        void UpdateDocument(DocumentDTO dto);

        void UpdateFolders(WorkspaceDTO dto);

        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
