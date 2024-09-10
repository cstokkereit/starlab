using StarLab.Commands; 
using StarLab.Presentation;
using StarLab.Presentation.Workspaces.WorkspaceExplorer;

namespace StarLab.UI.Workspaces.WorkspaceExplorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WorkspaceExplorerView : ControlView, IWorkspaceExplorerView
    {
        private readonly IDictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();

        private readonly IWorkspaceExplorerViewPresenter presenter;

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceExplorerView"/> class.
        /// </summary>
        /// <param name="presenterFactory">An <see cref="IPresenterFactory"/> that is used to create the <see cref="IPresenter"/> that controls this view.</param>
        public WorkspaceExplorerView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = (IWorkspaceExplorerViewPresenter)presenterFactory.CreatePresenter(this);

            AttachEventHandlers();
        }

        #region IWorkspaceExplorerView Members

        public void AddImage(string key, Image image)
        {
            imageList.Images.Add(key, image);
        }

        public void AddDocumentNode(string key, string parentKey, string text, string imageKey)
        {
            var parent = nodes[parentKey];
            var node = parent.Nodes.Add(key, text, imageKey, imageKey);
            node.Tag = Constants.DOCUMENT;
            nodes.Add(key, node);
        }

        public void AddFolderNode(string key, string parentKey, string text, string imageKey)
        {
            var parent = nodes[parentKey];
            var node = parent.Nodes.Add(key, text, imageKey, imageKey);
            node.Tag = Constants.FOLDER;
            nodes.Add(key, node);
        }

        public void AddRootNode(string key, string text, string imageKey)
        {
            var node = treeView.Nodes.Add(key, text, imageKey, imageKey);
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
            treeView.Nodes.Clear();
            nodes.Clear();
        }

        public void Collapse(string key)
        {
            if (nodes.ContainsKey(key)) nodes[key].Collapse();
        }

        public void EditNodeLabel(string key)
        {
            if (nodes.ContainsKey(key)) nodes[key].BeginEdit();
        }

        public void Expand(string key)
        {
            if (nodes.ContainsKey(key)) nodes[key].Expand();
        }

        public string GetSelectedNode()
        {
            return treeView.SelectedNode.Name;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/> </param>
        public override void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);

            treeView.ContextMenuStrip = new Controls.ContextMenuStrip();

            treeView.ContextMenuManager.Add(new WorkspaceMenuManager());
            treeView.ContextMenuManager.Add(new DocumentMenuManager());
            treeView.ContextMenuManager.Add(new FolderMenuManager());
        }

        #endregion

        #region Private Members

        private void AttachEventHandlers()
        {
            treeView.AfterCollapse += treeView_AfterCollapse;
            treeView.AfterExpand += treeView_AfterExpand;
            treeView.AfterSelect += treeView_AfterSelect;
            //treeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;
            treeView.ValidateLabelEdit += treeView_ValidateLabelEdit;
        }

        #endregion

        #region Event Handlers

        private void treeView_AfterCollapse(object? sender, TreeViewEventArgs? e)
        {
            if (e != null && e.Node != null) e.Node.ImageKey = presenter.GetImageKey((string)e.Node.Tag, e.Node.IsExpanded);
        }

        private void treeView_AfterExpand(object? sender, TreeViewEventArgs? e)
        {
            if (e != null && e.Node != null) e.Node.ImageKey = presenter.GetImageKey((string)e.Node.Tag, e.Node.IsExpanded);
        }

        private void treeView_AfterSelect(object? sender, TreeViewEventArgs? e)
        {
            //presenter.NodeSelected(e.Node.Name);
        }

        private void treeView_ValidateLabelEdit(object? sender, NodeLabelEditEventArgs? e)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
