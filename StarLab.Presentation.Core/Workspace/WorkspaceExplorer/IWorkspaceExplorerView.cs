using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// Defines the properties and methods used by the <see cref="IWorkspaceExplorerViewPresenter"/> to control the behaviour of the Workspace Explorer tool window.
    /// </summary>
    public interface IWorkspaceExplorerView : IChildView
    {
        /// <summary>
        /// Adds an <see cref="Image"/> to the list of available images.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to be added.</param>
        /// <returns>The index that can be used to select the <see cref="Image"/> from the list of available images.</returns>
        int AddImage(Image image);

        /// <summary>
        /// Adds a document node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the node image.</param>
        void AddDocumentNode(string key, string parentKey, string text, int imageIndex);

        /// <summary>
        /// Adds a folder node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the image to use when the node is not selected.</param>
        /// <param name="selectedImageIndex">The index of the image to use when the node is selected.</param>
        void AddFolderNode(string key, string parentKey, string text, int imageIndex, int selectedImageIndex);

        /// <summary>
        /// Adds a project node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the node image.</param>
        void AddProjectNode(string key, string parentKey, string text, int imageIndex);

        /// <summary>
        /// Adds the workspace node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the node image.</param>
        void AddWorkspaceNode(string key, string text, int imageIndex);

        /// <summary>
        /// Adds a toolbar button to the Workspace Explorer toolstrip.
        /// </summary>
        /// <param name="name">The name of the button to be added.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"/> that represents the button.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the button is clicked.</param>
        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);

        /// <summary>
        /// Clears the tree view.
        /// </summary>
        void Clear();

        /// <summary>
        /// Collapses the node with the specified key.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void CollapseNode(string key);

        /// <summary>
        /// Gets the default location for the Workspace Explorer tool window.
        /// </summary>
        string DefaultLocation { get; }

        /// <summary>
        /// Initiates editing of the specified tree node label.
        /// </summary>
        /// <param name="key">The node key.</param>
        void EditNodeLabel(string key);

        /// <summary>
        /// Expands the node with the specified key.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        void ExpandNode(string key);

        /// <summary>
        /// Sets the focus to the currently selected node.
        /// </summary>
        void FocusOnSelectedNode();

        /// <summary>
        /// Gets the key of the currently selected node.
        /// </summary>
        /// <returns>The key that identifies the selected node.</returns>
        string GetSelectedNode();

        /// <summary>
        /// Selects the node with the specified key.
        /// </summary>
        /// <param name="key">The node key.</param>
        void SelectNode(string key);

        /// <summary>
        /// Sets the text displayed in the label of the specified node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="text">The label text.</param>
        void SetNodeText(string key, string text);

        /// <summary>
        /// Updates the images to be used for the selected and unselected states of the specified node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="imageIndex">The index of the image to use when the node is not selected.</param>
        /// <param name="selectedImageIndex">The index of the image to use when the node is selected.</param>
        void UpdateNodeState(string key, int imageIndex, int selectedImageIndex);
    }
}
