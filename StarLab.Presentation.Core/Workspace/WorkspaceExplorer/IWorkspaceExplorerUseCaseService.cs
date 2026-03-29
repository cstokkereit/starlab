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
        /// <param name="key">The key that identifies the parent folder.</param>
        void AddFolder(IWorkspace workspace, string key);

        /// <summary>
        /// Executes the Copy use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the document or folder to be copied.</param>
        void Copy(IWorkspace workspace, string key);

        /// <summary>
        /// Executes the Cut use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the document or folder to be cut.</param>
        void Cut(IWorkspace workspace, string key);

        /// <summary>
        /// Executes the DeleteDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="id">The ID of the document to be deleted.</param>
        void DeleteDocument(IWorkspace workspace, string id);

        /// <summary>
        /// Executes the DeleteFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the folder to be deleted.</param>
        void DeleteFolder(IWorkspace workspace, string key);

        /// <summary>
        /// Executes the Paste use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the destination for the document or folder.</param>
        void Paste(IWorkspace workspace, string key);

        /// <summary>
        /// Executes the RenameDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        void RenameDocument(IWorkspace workspace, string key, string name);

        /// <summary>
        /// Executes the RenameFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        void RenameFolder(IWorkspace workspace, string key, string name);

        /// <summary>
        /// Executes the RenameWorkspace use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="name">The new name.</param>
        void RenameWorkspace(IWorkspace workspace, string name);
    }
}
