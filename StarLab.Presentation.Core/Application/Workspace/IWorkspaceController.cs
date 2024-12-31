namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a controller that can be used to control the workspace.
    /// </summary>
    public interface IWorkspaceController : IViewController
    {
        /// <summary>
        /// Adds a folder with the specified parent folder.
        /// </summary>
        /// <param name="key">The key that identifies the parent folder.</param>
        void AddFolder(string key);

        /// <summary>
        /// Closes the active document.
        /// </summary>
        void CloseActiveDocument();

        /// <summary>
        /// Closes the workspace.
        /// </summary>
        void CloseWorkspace();

        /// <summary>
        /// Deletes the document with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the document to be deleted.</param>
        void DeleteDocument(string id);

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="key">The key that identifies the folder to be deleted.</param>
        void DeleteFolder(string key);

        /// <summary>
        /// Exists the application.
        /// </summary>
        void Exit();

        /// <summary>
        /// Creates a new workspace.
        /// </summary>
        void NewWorkspace();

        /// <summary>
        /// Opens a workspace.
        /// </summary>
        void OpenWorkspace();

        /// <summary>
        /// Renames the specified document.
        /// </summary>
        /// <param name="id">The ID of the document to be renamed.</param>
        /// <param name="name">The new name.</param>
        void RenameDocument(string id, string name);

        /// <summary>
        /// Renames the specified folder.
        /// </summary>
        /// <param name="key">The key that identifies the folder to be renamed.</param>
        /// <param name="name">The new name.</param>
        void RenameFolder(string key, string name);

        /// <summary>
        /// Renames the workspace.
        /// </summary>
        /// <param name="name">The new name.</param>
        void RenameWorkspace(string name);

        /// <summary>
        /// Saves the workspace.
        /// </summary>
        void SaveWorkspace();
    }
}
