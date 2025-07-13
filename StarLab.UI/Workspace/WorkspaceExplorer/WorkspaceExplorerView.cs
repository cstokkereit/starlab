using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.WorkspaceExplorer;
using StarLab.UI.Controls;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the Workspace Explorer tool.
    /// </summary>
    public partial class WorkspaceExplorerView : UserControl, IWorkspaceExplorerView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WorkspaceExplorerView)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>(); // A dictionary containing the tree nodes indexed by node key.

        private readonly IWorkspaceExplorerViewPresenter presenter; // The presenter that controls the view.

        private readonly SplitViewPanels panel; // The panel that will contain the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceExplorerView"/> class.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the configuration information required to construct this view.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        public WorkspaceExplorerView(IViewDefinition definition, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.WorkspaceExplorer;

            panel = (SplitViewPanels)definition.Panel;

            presenter = (IWorkspaceExplorerViewPresenter)factory.CreatePresenter(this);
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"> that controls this view.
        /// </summary>
        public IChildViewController Controller => (IChildViewController)presenter;

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        public SplitViewPanels Panel => panel;

        /// <summary>
        /// Adds an <see cref="Image"/> to the list of available images.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> to be added.</param>
        /// <returns>The index that can be used to select the <see cref="Image"/> from the list of available images.</returns>
        public int AddImage(Image image)
        {
            int index = imageList.Images.Count;
            imageList.Images.Add(image);
            return index;
        }

        /// <summary>
        /// Adds a document node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the node image.</param>
        /// <param name="selectedImageIndex">The index of the image to use when the node is selected.</param>
        public void AddDocumentNode(string key, string parentKey, string text, int imageIndex, int selectedImageIndex)
        {
            var parent = nodes[parentKey];
            var node = parent.Nodes.Add(key, text, imageIndex, selectedImageIndex);
            node.Tag = Constants.Document;
            nodes.Add(key, node);
        }

        /// <summary>
        /// Adds a folder node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the image to use when the node is not selected.</param>
        /// <param name="selectedImageIndex">The index of the image to use when the node is selected.</param>
        public void AddFolderNode(string key, string parentKey, string text, int imageIndex, int selectedImageIndex)
        {
            var parent = nodes[parentKey];
            var node = parent.Nodes.Add(key, text, imageIndex, selectedImageIndex);
            node.Tag = Constants.Folder;
            nodes.Add(key, node);
        }

        /// <summary>
        /// Adds a project node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the node image.</param>
        /// <param name="selectedImageIndex">The index of the image to use when the node is selected.</param>
        public void AddProjectNode(string key, string parentKey, string text, int imageIndex, int selectedImageIndex)
        {
            var parent = nodes[parentKey];
            var node = parent.Nodes.Add(key, text, imageIndex, selectedImageIndex);
            node.Tag = Constants.Project;
            nodes.Add(key, node);
        }

        /// <summary>
        /// Adds the workspace node to the tree view that displays the structure of the workspace.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="text">The node text.</param>
        /// <param name="imageIndex">The index of the node image.</param>
        /// <param name="selectedImageIndex">The index of the image to use when the node is selected.</param>
        public void AddWorkspaceNode(string key, string text, int imageIndex, int selectedImageIndex)
        {
            var node = treeView.Nodes.Add(key, text, imageIndex, selectedImageIndex);
            node.Tag = Constants.Workspace;
            nodes.Add(key, node);
        }

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The image to use for the button.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            toolStrip.AddButton(name, tooltip, image, command);
        }

        /// <summary>
        /// Clears the tree view.
        /// </summary>
        public void Clear()
        {
            treeView.ContextMenuStrip = null;
            treeView.Nodes.Clear();
            nodes.Clear();
        }

        /// <summary>
        /// Collapses the node with the specified key.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        public void CollapseNode(string key)
        {
            if (nodes.ContainsKey(key)) nodes[key].Collapse();
        }

        /// <summary>
        /// Initiates editing of the specified tree node label.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void EditNodeLabel(string key)
        {
            if (nodes.ContainsKey(key))
            {
                treeView.LabelEdit = true;
                nodes[key].BeginEdit();
            }
        }

        /// <summary>
        /// Expands the node with the specified key.
        /// </summary>
        /// <param name="key">The key that identifies the node.</param>
        public void ExpandNode(string key)
        {
            if (nodes.ContainsKey(key)) nodes[key].Expand();
        }

        /// <summary>
        /// Sets the focus to the currently selected node.
        /// </summary>
        public void FocusOnSelectedNode()
        {
            treeView.Focus();
        }

        /// <summary>
        /// Gets the key of the currently selected node.
        /// </summary>
        /// <returns>The key that identifies the selected node.</returns>
        public string GetSelectedNode()
        {
            return treeView.SelectedNode == null ? string.Empty : treeView.SelectedNode.Name;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            // Do Nothing
        }

        /// <summary>
        /// Selects the node with the specified key.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void SelectNode(string key)
        {
            if (nodes.ContainsKey(key)) treeView.SelectedNode = nodes[key];
        }

        /// <summary>
        /// Sets the text displayed in the label of the specified node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="text">The label text.</param>
        public void SetNodeText(string key, string text)
        {
            if (nodes.ContainsKey(key)) nodes[key].Text = text;
        }

        /// <summary>
        /// Updates the images to be used for the selected and unselected states of the specified node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="imageIndex">The index of the image to use when the node is not selected.</param>
        /// <param name="selectedImageIndex">The index of the image to use when the node is selected.</param>
        public void UpdateNodeState(string key, int imageIndex, int selectedImageIndex)
        {
            if (nodes.ContainsKey(key))
            {
                var node = nodes[key];
                node.SelectedImageIndex = selectedImageIndex;
                node.ImageIndex = imageIndex;
            }
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.AfterCollapse"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="TreeViewEventArgs"/> that provides context for the event.</param>
        private void TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                var node = e.Node;

                switch (GetNodeType(node))
                {
                    case Constants.Folder:
                        presenter.FolderCollapsed(node.Name);
                        break;

                    case Constants.Project:
                        presenter.ProjectCollapsed(node.Name);
                        break;

                    case Constants.Workspace:
                        presenter.WorkspaceCollapsed();
                        break;
                }
            }
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.AfterExpand"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="TreeViewEventArgs"/> that provides context for the event.</param>
        private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                var node = e.Node;

                switch (GetNodeType(node))
                {
                    case Constants.Folder:
                        presenter.FolderExpanded(node.Name);
                        break;

                    case Constants.Project:
                        presenter.ProjectExpanded(node.Name);
                        break;

                    case Constants.Workspace:
                        presenter.WorkspaceExpanded();
                        break;
                }
            }
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.AfterLabelEdit"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="NodeLabelEditEventArgs"/> that provides context for the event.</param>
        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e != null && e.Label != null && e.Node != null)
            {
                try
                {
                    var node = e.Node;

                    switch (GetNodeType(node))
                    {
                        case Constants.Document:
                            presenter.RenameDocument(e.Node.Name, e.Label);
                            break;

                        case Constants.Folder:
                        case Constants.Project:
                            presenter.RenameFolder(e.Node.Name, e.Label);
                            break;

                        case Constants.Workspace:
                            presenter.RenameWorkspace(e.Label);
                            break;
                    }

                    treeView.LabelEdit = false;
                }
                catch (Exception ex)
                {
                    e.CancelEdit = true;

                    presenter.ShowMessage(ex.Message);

                    e.Node.BeginEdit();
                }
            }
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.Enter"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void TreeView_Enter(object sender, EventArgs e)
        {
            presenter.ViewActivated();
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.Leave"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void TreeView_Leave(object sender, EventArgs e)
        {
            presenter.ViewDeactivated();
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.NodeMouseDoubleClick"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="TreeNodeMouseClickEventArgs"/> that provides context for the event.</param>
        private void TreeView_NodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                if (GetNodeType(e.Node) == Constants.Document) presenter.OpenDocument(e.Node.Name);
            }
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.NodeMouseDoubleClick"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">A <see cref="TreeNodeMouseClickEventArgs"/> that provides context for the event.</param>
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e != null && e.Button == MouseButtons.Right)
            {
                var menu = new ManagedContextMenuStrip();

                var node = treeView.GetNodeAt(e.X, e.Y);

                switch (GetNodeType(node))
                {
                    case Constants.Document:
                        presenter.CreateDocumentContextMenu(node.Name, menu);
                        break;

                    case Constants.Folder:
                        presenter.CreateFolderContextMenu(node.Name, menu);
                        break;

                    case Constants.Project:
                        presenter.CreateProjectContextMenu(node.Name, menu);
                        break;

                    case Constants.Workspace:
                        presenter.CreateWorkspaceContextMenu(menu);
                        break;
                }

                treeView.ContextMenuStrip = menu;
            }
        }

        /// <summary>
        /// Gets the node type for the <see cref="TreeNode"> provided.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode"/> for which the node type is required.</param>
        /// <returns>The node type of the <see cref="TreeNode"/> provided.</returns>
        private string GetNodeType(TreeNode node)
        {
            return node == null ? string.Empty : (string)node.Tag;
        }
    }
}
