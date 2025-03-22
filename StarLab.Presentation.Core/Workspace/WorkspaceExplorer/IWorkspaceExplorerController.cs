namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// Represents a controller that can be used to control the workspace explorer.
    /// </summary>
    public interface IWorkspaceExplorerController : IChildViewController
    {
        /// <summary>
        /// Adds a folder with the specified parent folder.
        /// </summary>
        /// <param name="key">The key that identifies the parent folder.</param>
        void AddFolder(string key);

        /// <summary>
        /// Collapses the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The key that identifies the node to be collapsed; the workspace, a project or folder.</param>
        void Collapse(string key);

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
        /// Deletes the specified project.
        /// </summary>
        /// <param name="key">The key that identifies the project to be deleted.</param>
        void DeleteProject(string key);

        /// <summary>
        /// Renames the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The key that identifies the node to be renamed; the workspace, a project, folder or document.</param>
        void Rename(string key);

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The ID of the document to open.</param>
        void OpenDocument(string id);

        /// <summary>
        /// Selects node in the workspace hierarchy that corresponds to the active document.
        /// </summary>
        void Synchronise();
    }
}
