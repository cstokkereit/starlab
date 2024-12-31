namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// Defines the methods used by the <see cref="IWorkspaceExplorerView"/> to communicate with its presenter.
    /// </summary>
    public interface IWorkspaceExplorerViewPresenter : IChildViewPresenter
    {
        /// <summary>
        /// Notifies the presenter that the specified document node has been selected.
        /// </summary>
        /// <param name="id">The ID of the node.</param>
        void DocumentSelected(string id);

        /// <summary>
        /// Notifies the presenter that the specified folder node has been collapsed.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void FolderCollapsed(string key);

        /// <summary>
        /// Notifies the presenter that the specified folder node has been expanded.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void FolderExpanded(string key);

        /// <summary>
        /// Notifies the presenter that the specified folder node has been selected.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void FolderSelected(string key);

        /// <summary>
        /// Gets the index of the image that will be used to represent the specified node in the workspace hierarchy based on the type of node and its current state.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="expanded">A flag that indicates whether the node is collapsed or expanded.</param>
        /// <param name="selected">A flag that indicates whether the node is selected or not.</param>
        /// <returns>The index of the image to be used.</returns>
        int GetImageIndex(string nodeType, bool expanded, bool selected);

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The ID of the document to open.</param>
        void OpenDocument(string id);

        /// <summary>
        /// Notifies the presenter that the specified project node has been collapsed.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void ProjectCollapsed(string key);

        /// <summary>
        /// Notifies the presenter that the specified project node has been expanded.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void ProjectExpanded(string key);

        /// <summary>
        /// Notifies the presenter that the specified project node has been selected.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void ProjectSelected(string key);

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
        /// Displays a <see cref="MessageBox"/> with the specified message.
        /// </summary>
        /// <param name="message">The message text.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Notifies the presenter that the view has been activated.
        /// </summary>
        void ViewActivated();

        /// <summary>
        /// Notifies the presenter that the view has been deactivated.
        /// </summary>
        void ViewDeactivated();

        /// <summary>
        /// Notifies the presenter that the workspace node has been collapsed.
        /// </summary>
        void WorkspaceCollapsed();

        /// <summary>
        /// Notifies the presenter that the workspace node has been expanded.
        /// </summary>
        void WorkspaceExpanded();

        /// <summary>
        /// Notifies the presenter that the workspace node has been selected.
        /// </summary>
        void WorkspaceSelected();
    }
}
