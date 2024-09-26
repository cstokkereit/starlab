using StarLab.Commands;
using StarLab.Presentation;
using System.Xml.Linq;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WorkspaceExplorerView : ControlView, IWorkspaceExplorerView
    {
        private readonly Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();

        private readonly IWorkspaceExplorerViewPresenter presenter;

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceExplorerView"/> class.
        /// </summary>
        /// <param name="presenterFactory">An <see cref="IPresenterFactory"/> that is used to create the <see cref="IPresenter"/> that controls this view.</param>
        public WorkspaceExplorerView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = (IWorkspaceExplorerViewPresenter)presenterFactory.CreatePresenter(this);

            treeView.ContextMenuStrip = new ContextMenuStrip();
        }

        #region IWorkspaceExplorerView Members

        public int AddImage(Image image)
        {
            int index = imageList.Images.Count;
            imageList.Images.Add(image);
            return index;
        }

        public void AddDocumentNode(string key, string parentKey, string text, int imageIndex)
        {
            var parent = nodes[parentKey];
            var node = parent.Nodes.Add(key, text, imageIndex, imageIndex);
            node.Tag = Constants.DOCUMENT;
            nodes.Add(key, node);
        }

        public void AddFolderNode(string key, string parentKey, string text, int unselectedImageIndex, int selectedImageIndex)
        {
            var parent = nodes[parentKey];
            var node = parent.Nodes.Add(key, text, unselectedImageIndex, selectedImageIndex);
            node.Tag = Constants.FOLDER;
            nodes.Add(key, node);
        }

        public void AddRootNode(string key, string text, int imageIndex)
        {
            var node = treeView.Nodes.Add(key, text, imageIndex, imageIndex);
            node.Tag = Constants.WORKSPACE;
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
        /// 
        /// </summary>
        public void Clear()
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                if ((string)node.Tag == Constants.DOCUMENT)
                {
                    treeView.ContextMenuManager.Remove(node.Name); // Remove method if never called
                }
            }

            treeView.Nodes.Clear();
            nodes.Clear();
        }

        public void CollapseNode(string key)
        {
            if (nodes.ContainsKey(key)) nodes[key].Collapse();
        }

        public IMenuManager CreateDocumentMenuManager(string document)
        {
            var manager = new DocumentMenuManager(document);
            treeView.ContextMenuManager.Add(manager);
            return manager;
        }

        public IMenuManager CreateFolderMenuManager(string folder)
        {
            var manager = new FolderMenuManager(folder);
            treeView.ContextMenuManager.Add(manager);
            return manager;
        }

        public IMenuManager CreateWorkspaceMenuManager()
        {
            var manager = new WorkspaceMenuManager(Constants.WORKSPACE);
            treeView.ContextMenuManager.Add(manager);
            return manager;
        }

        public void EditNodeLabel(string key)
        {
            if (nodes.ContainsKey(key))
            {
                treeView.LabelEdit = true;
                nodes[key].BeginEdit();
            }
        }

        public void ExpandNode(string key)
        {
            if (nodes.ContainsKey(key)) nodes[key].Expand();
        }

        public string GetSelectedNode()
        {
            return treeView.SelectedNode == null ? string.Empty : treeView.SelectedNode.Name;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/> </param>
        public override void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);
        }

        public void UpdateNodeState(string key, int imageIndex, int selectedImageIndex)
        {
            if (nodes.ContainsKey(key))
            {
                var node = nodes[key];

                node.SelectedImageIndex = selectedImageIndex;
                node.ImageIndex = imageIndex;
            }
        }

        #endregion

        #region Event Handlers

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                var node = e.Node;

                switch (GetNodeType(node))
                {
                    case Constants.FOLDER:
                        presenter.FolderCollapsed(node.Name);
                        node.SelectedImageIndex = presenter.GetImageIndex(Constants.FOLDER, node.IsExpanded, true);
                        node.ImageIndex = presenter.GetImageIndex(Constants.FOLDER, node.IsExpanded, false);
                        break;

                    case Constants.WORKSPACE:
                        presenter.WorkspaceCollapsed();
                        break;
                }
            }
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                var node = e.Node;

                switch (GetNodeType(node))
                {
                    case Constants.FOLDER:
                        presenter.FolderExpanded(node.Name);
                        node.SelectedImageIndex = presenter.GetImageIndex(Constants.FOLDER, node.IsExpanded, true);
                        node.ImageIndex = presenter.GetImageIndex(Constants.FOLDER, node.IsExpanded, false);
                        break;

                    case Constants.WORKSPACE:
                        presenter.WorkspaceExpanded();
                        break;
                }
            }
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e != null && e.Label != null && e.Node != null)
            {
                var node = e.Node;

                switch (GetNodeType(node))
                {
                    case Constants.DOCUMENT:
                        presenter.RenameDocument(e.Node.Name, e.Label);
                        break;

                    case Constants.FOLDER:
                        presenter.RenameFolder(e.Node.Name, e.Label);
                        break;
                }
                
                treeView.LabelEdit = false;
            }
        }

        private void treeView_Enter(object sender, EventArgs e)
        {
            presenter.ViewActivated();
        }

        private void treeView_Leave(object sender, EventArgs e)
        {
            presenter.ViewDeactivated();
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e != null)
            {
                var node = treeView.GetNodeAt(e.X, e.Y);

                switch (GetNodeType(node))
                {
                    case Constants.DOCUMENT:
                        presenter.DocumentSelected(node.Name);
                        break;

                    case Constants.FOLDER:
                        presenter.FolderSelected(node.Name);
                        break;

                    case Constants.WORKSPACE:
                        presenter.WorkspaceSelected();
                        break;
                }
            }
        }

        private void treeView_NodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                if (GetNodeType(e.Node) == Constants.DOCUMENT) presenter.OpenDocument(e.Node.Name);
            }
        }

        #endregion

        private string GetNodeType(TreeNode node)
        {
            return node == null ? string.Empty : (string)node.Tag;
        }
    }
}
