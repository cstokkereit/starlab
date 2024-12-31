namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// Represents a controller that can be used to control the workspace explorer.
    /// </summary>
    public interface IWorkspaceExplorerController : IChildViewController
    {
        /// <summary>
        /// Collapses the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The key that identifies the node to be collapsed; the workspace, a project or folder.</param>
        void Collapse(string key);

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
