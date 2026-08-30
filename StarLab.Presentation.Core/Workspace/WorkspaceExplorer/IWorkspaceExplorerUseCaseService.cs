using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    ///  Defines the methods required to execute the use cases that implement the workspace explorer functionality.
    /// </summary>
    public interface IWorkspaceExplorerUseCaseService
    {
        /// <summary>
        /// Executes the AddFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="path">The path to the parent folder.</param>
        void AddFolder(IWorkspace workspace, string path);

        /// <summary>
        /// Executes the DeleteDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="id">The ID of the document to be deleted.</param>
        void DeleteDocument(IWorkspace workspace, DocumentID id);

        /// <summary>
        /// Executes the DeleteFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="path">The path to the folder to be deleted.</param>
        void DeleteFolder(IWorkspace workspace, string key);

        /// <summary>
        /// Executes the CopyAndPaste use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="source">The key that identifies the source document or folder.</param>
        /// <param name="destination">The key that identifies the destination document or folder.</param>
        void CopyAndPaste(IWorkspace workspace, string source, string destination);

        /// <summary>
        /// Executes the CutAndPaste use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="source">The key that identifies the source document or folder.</param>
        /// <param name="destination">The key that identifies the destination document or folder.</param>
        void CutAndPaste(IWorkspace workspace, string source, string destination);

        /// <summary>
        /// Executes the RenameDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="id">The document ID.</param>
        /// <param name="name">The new name.</param>
        void RenameDocument(IWorkspace workspace, DocumentID id, string name);

        /// <summary>
        /// Executes the RenameFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="path">The folder path.</param>
        /// <param name="name">The new name.</param>
        void RenameFolder(IWorkspace workspace, string path, string name);

        /// <summary>
        /// Executes the RenameWorkspace use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="name">The new name.</param>
        void RenameWorkspace(IWorkspace workspace, string name);
    }
}
