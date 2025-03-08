using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IWorkspaceExplorerView"/>.
    /// </summary>
    internal class WorkspaceExplorerViewPresenter : ChildViewPresenter<IWorkspaceExplorerView, IViewController>, IWorkspaceExplorerViewPresenter, IWorkspaceExplorerController, ISubscriber<ActiveDocumentChangedEventArgs>, ISubscriber<WorkspaceChangedEventArgs>
    {
        /// <summary>
        /// Provides constants that can be used to access the node image indices.
        /// </summary>
        private enum NodeImages
        {
            ClosedFolder,
            ColourMagnitudeDiagram,
            OpenFolder,
            SelectedClosedFolder,
            SelectedOpenFolder,
            Workspace
        }

        private readonly Dictionary<NodeImages, int> images = new Dictionary<NodeImages, int>(); // A dictionary that holds the node image indices.

        private IWorkspace workspace; // The workspace that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceExplorerViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IWorkspaceExplorerView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IConfigurationProvider"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public WorkspaceExplorerViewPresenter(IWorkspaceExplorerView view, ICommandManager commands, IUseCaseFactory factory, Configuration.IConfigurationProvider configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, configuration, mapper, events)
        {
            workspace = new EmptyWorkspace();
        }

        /// <summary>
        /// Adds a chart to the specified workspace folder.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        public void AddChart(string path)
        {
            if (AppController.GetView(Views.ADD_DOCUMENT) is IDialog dialog)
            {
                dialog.Show(new AddDocumentInteractionContext(workspace, path, DocumentType.Chart));
            }   
        }

        /// <summary>
        /// Collapses the workspace, project or folder node with the specified key.
        /// </summary>
        /// <param name="key">The workspace, project or folder key.</param>
        public void Collapse(string key)
        {
            if (key.Equals(Constants.WORKSPACE))
            {
                workspace.Collapse();
            }
            else if (workspace.HasProject(key))
            {
                workspace.GetProject(key).CollapseAll();
            }
            else if (workspace.HasFolder(key))
            {
                workspace.GetFolder(key).CollapseAll();
            }

            UpdateNodes();
        }

        /// <summary>
        /// Notifies the presenter that the specified document node has been selected.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void DocumentSelected(string key)
        {
            DeselectFolders();
        }

        /// <summary>
        /// Notifies the presenter that the specified folder node has been collapsed.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void FolderCollapsed(string key)
        {
            workspace.GetFolder(key).Collapse();
        }

        /// <summary>
        /// Notifies the presenter that the specified folder node has been expanded.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void FolderExpanded(string key)
        {
            workspace.GetFolder(key).Expand();
        }

        /// <summary>
        /// Notifies the presenter that the specified folder node has been selected.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void FolderSelected(string key)
        {
            int imageIndex;

            foreach (var folder in workspace.Folders)
            {
                if (folder.Key == key)
                {
                    imageIndex = GetImageIndex(Constants.FOLDER, folder.Expanded, true);
                    View.UpdateNodeState(folder.Key, imageIndex, imageIndex);
                }
                else
                {
                    imageIndex = GetImageIndex(Constants.FOLDER, folder.Expanded, false);
                    View.UpdateNodeState(folder.Key, imageIndex, imageIndex);
                }
            }
        }

        /// <summary>
        /// Gets the index of the image that will be used to represent the specified node in the workspace hierarchy based on the type of node and its current state.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <param name="expanded">A flag that indicates whether the node is collapsed or expanded.</param>
        /// <param name="selected">A flag that indicates whether the node is selected or not.</param>
        /// <returns>The index of the image to be used.</returns>
        public int GetImageIndex(string nodeType, bool expanded, bool selected)
        {
            int index = images[NodeImages.Workspace];

            if (nodeType == Constants.FOLDER)
            {
                if (selected)
                {
                    index = expanded ? images[NodeImages.SelectedOpenFolder] : images[NodeImages.SelectedClosedFolder];
                }
                else
                {
                    index = expanded ? images[NodeImages.OpenFolder] : images[NodeImages.ClosedFolder];
                }  
            }

            return index;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                AddImages();
                CreateToolbar();
            }
        }

        /// <summary>
        /// Event handler for the ActiveDocumentChangedEvent event.
        /// </summary>
        /// <param name="args">An <see cref="ActiveDocumentChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(ActiveDocumentChangedEventArgs args)
        {
            UpdateCommandState(Actions.SYNCHRONISE, args.Workspace.ActiveDocument != null);
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            UpdateWorkspace(args.Workspace);
        }

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void OpenDocument(string key)
        {
            AppController.Show(key);
        }

        /// <summary>
        /// Notifies the presenter that the specified project node has been collapsed.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void ProjectCollapsed(string key)
        {
            workspace.GetProject(key).Collapse();
        }

        /// <summary>
        /// Notifies the presenter that the specified project node has been expanded.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void ProjectExpanded(string key)
        {
            workspace.GetProject(key).Expand();
        }

        /// <summary>
        /// Notifies the presenter that the specified project node has been selected.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void ProjectSelected(string key)
        {
            DeselectFolders(); // TODO - Is this correct?
        }

        /// <summary>
        /// Renames the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The key of the node to be renamed.</param>
        public void Rename(string key)
        {
            if (key == Constants.WORKSPACE)
            {
                View.SetNodeText(key, workspace.Name);
                View.EditNodeLabel(key);
            }
            else
            {
                View.EditNodeLabel(key);
            }
        }

        /// <summary>
        /// Renames the specified document.
        /// </summary>
        /// <param name="id">The node key.</param>
        /// <param name="name">The new name.</param>
        public void RenameDocument(string key, string name)
        {
            AppController.GetWorkspaceController().RenameDocument(key, name);
        }

        /// <summary>
        /// Renames the specified folder.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        public void RenameFolder(string key, string name)
        {
            AppController.GetWorkspaceController().RenameFolder(key, name);
        }

        /// <summary>
        /// Renames the workspace.
        /// </summary>
        /// <param name="name">The new name.</param>
        public void RenameWorkspace(string name)
        {
            AppController.GetWorkspaceController().RenameWorkspace(name);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified message.
        /// </summary>
        /// <param name="message">The message text.</param>
        public void ShowMessage(string message)
        {
            ShowMessage(StringResources.StarLab, message, InteractionType.Error, InteractionResponses.OK);
        }

        /// <summary>
        /// Selects the node that represents the active document.
        /// </summary>
        public void Synchronise()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notifies the presenter that the view has been activated.
        /// </summary>
        public void ViewActivated()
        {
            var key = View.GetSelectedNode();

            if (workspace.HasFolder(key))
            {
                var folder = workspace.GetFolder(key);

                if (folder.Expanded)
                {
                    View.UpdateNodeState(folder.Key, images[NodeImages.OpenFolder], images[NodeImages.SelectedOpenFolder]);
                }   
                else
                {
                    View.UpdateNodeState(folder.Key, images[NodeImages.ClosedFolder], images[NodeImages.SelectedClosedFolder]);
                }   
            }
        }

        /// <summary>
        /// Notifies the presenter that the view has been deactivated.
        /// </summary>
        public void ViewDeactivated()
        {
            var key = View.GetSelectedNode();

            if (workspace.HasFolder(key))
            {
                var folder = workspace.GetFolder(key);

                if (folder.Expanded)
                {
                    View.UpdateNodeState(folder.Key, images[NodeImages.OpenFolder], images[NodeImages.OpenFolder]);
                }
                else
                {
                    View.UpdateNodeState(folder.Key, images[NodeImages.ClosedFolder], images[NodeImages.ClosedFolder]);
                }   
            }
        }

        /// <summary>
        /// Notifies the presenter that the workspace node has been collapsed.
        /// </summary>
        public void WorkspaceCollapsed()
        {
            workspace.Collapse();
        }

        /// <summary>
        /// Notifies the presenter that the workspace node has been expanded.
        /// </summary>
        public void WorkspaceExpanded()
        {
            workspace.Expand();
        }

        /// <summary>
        /// Notifies the presenter that the workspace node has been selected.
        /// </summary>
        public void WorkspaceSelected()
        {
            DeselectFolders();
        }

        /// <summary>
        /// Adds the images that represent the various node types and their states.
        /// </summary>
        private void AddImages()
        {
            images.Add(NodeImages.Workspace, View.AddImage(ImageResources.Workspace));
            images.Add(NodeImages.ClosedFolder, View.AddImage(ImageResources.FolderClosed));
            images.Add(NodeImages.SelectedClosedFolder, View.AddImage(ImageResources.SelectedFolderClosed));
            images.Add(NodeImages.OpenFolder, View.AddImage(ImageResources.FolderOpened));
            images.Add(NodeImages.SelectedOpenFolder, View.AddImage(ImageResources.SelectedFolderOpened));
            images.Add(NodeImages.ColourMagnitudeDiagram, View.AddImage(ImageResources.ColourMagnitudeDiagram));
        }

        /// <summary>
        /// Creates the menu manager responsible for generating the context menu for the node that represents the specified document.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that the node represents.</param>
        private void CreateDocumentMenuManager(IDocument document)
        {
            var manager = View.CreateDocumentMenuManager(document.ID);

            var workspaceController = AppController.GetWorkspaceController();

            manager.AddMenuItem(Constants.OPEN, StringResources.Open, ImageResources.Open, GetCommand(Actions.OPEN_DOCUMENT, document.ID));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete, GetCommand(workspaceController, Actions.DELETE_DOCUMENT, document.ID));
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.RENAME, document.ID));
        }

        /// <summary>
        /// Creates the nodes that represent the documents contained within the workspace hierarchy.
        /// </summary>
        private void CreateDocumentNodes()
        {
            foreach (var document in workspace.Documents)
            {
                View.AddDocumentNode(document.ID, document.Path, document.Name, images[NodeImages.ColourMagnitudeDiagram]);
                CreateDocumentMenuManager(document);
            }
        }

        /// <summary>
        /// Creates the menu manager responsible for generating the context menu for the node that represents the specified folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that the node represents.</param>
        private void CreateFolderMenuManager(IFolder folder)
        {
            var manager = View.CreateFolderMenuManager(folder.Key);

            var workspaceController = AppController.GetWorkspaceController();

            manager.AddMenuItem(Constants.ADD, StringResources.Add);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_CHART, StringResources.Chart + Constants.ELLIPSIS, GetCommand(this, Actions.ADD_CHART, folder.Key));
            manager.AddMenuItem(Constants.ADD, Constants.ADD_TABLE, StringResources.Table + Constants.ELLIPSIS, GetCommand(this, Actions.ADD_TABLE, folder.Key));
            manager.AddMenuItem(Constants.ADD, Constants.ADD_FOLDER, StringResources.NewFolder, ImageResources.NewFolder, GetCommand(workspaceController, Actions.ADD_FOLDER, folder.Key));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.COLLAPSE_ALL, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.COLLAPSE, folder.Key));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete, GetCommand(workspaceController, Actions.DELETE_FOLDER, folder.Key));
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.RENAME, folder.Key));
        }

        /// <summary>
        /// Creates the nodes that represent the folders contained within the workspace hierarchy.
        /// </summary>
        private void CreateFolderNodes()
        {
            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded)
                {
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, images[NodeImages.OpenFolder], images[NodeImages.SelectedOpenFolder]);
                }
                else
                {
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, images[NodeImages.ClosedFolder], images[NodeImages.SelectedClosedFolder]);
                }   

                CreateFolderMenuManager(folder);
            }
        }

        /// <summary>
        /// Creates the menu manager responsible for generating the context menu for the node that represents the specified project.
        /// </summary>
        /// <param name="project">The <see cref="IProject"/> that the node represents.</param>
        private void CreateProjectMenuManager(IProject project)
        {
            var manager = View.CreateProjectMenuManager(project.Key);

            var workspaceController = AppController.GetWorkspaceController();

            manager.AddMenuItem(Constants.ADD, StringResources.Add);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_CHART, StringResources.Chart + Constants.ELLIPSIS, GetCommand(workspaceController, Actions.ADD_CHART, project.Key));
            manager.AddMenuItem(Constants.ADD, Constants.ADD_TABLE, StringResources.Table + Constants.ELLIPSIS, GetCommand(workspaceController, Actions.ADD_TABLE, project.Key));
            manager.AddMenuItem(Constants.ADD, Constants.ADD_FOLDER, StringResources.NewFolder, ImageResources.NewFolder, GetCommand(workspaceController, Actions.ADD_FOLDER, project.Key));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.COLLAPSE_ALL, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.COLLAPSE, project.Key));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete, GetCommand(workspaceController, Actions.DELETE_FOLDER, project.Key));
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.RENAME, project.Key));
        }

        /// <summary>
        /// Creates the nodes that represent the projects contained within the workspace hierarchy.
        /// </summary>
        private void CreateProjectNodes()
        {
            foreach (var project in workspace.Projects)
            {
                View.AddProjectNode(project.Key, project.ParentKey, project.Name, images[NodeImages.Workspace]);
                CreateProjectMenuManager(project);
            }
        }

        /// <summary>
        /// Creates the Workspace Explorer toolbar.
        /// </summary>
        private void CreateToolbar()
        {
            View.AddToolbarButton(Constants.SYNCHRONISE, StringResources.SyncWithActiveDocument, ImageResources.Synchronise, GetCommand(Actions.SYNCHRONISE));
            View.AddToolbarButton(Constants.COLLAPSE_ALL, StringResources.CollapseAll, ImageResources.CollapseAll, GetCommand(Actions.COLLAPSE, Constants.WORKSPACE));
        }

        /// <summary>
        /// Creates the menu manager responsible for generating the context menu for the node that represents the workspace.
        /// </summary>
        private void CreateWorkspaceMenuManager()
        {
            var manager = View.CreateWorkspaceMenuManager();

            manager.AddMenuItem(Constants.COLLAPSE_ALL, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.COLLAPSE, Constants.WORKSPACE));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.ADD, StringResources.Add);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_PROJECT, StringResources.Project + Constants.ELLIPSIS);
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.RENAME, Constants.WORKSPACE));
        }

        /// <summary>
        /// Creates the node that represents the workspace.
        /// </summary>
        private void CreateWorkspaceNode()
        {
            View.AddWorkspaceNode(Constants.WORKSPACE, GetWorkspaceName(), images[NodeImages.Workspace]);
            CreateWorkspaceMenuManager();
        }

        /// <summary>
        /// Deselects the folders. TODO - May not be necessary?
        /// </summary>
        private void DeselectFolders()
        {
            int imageIndex;

            foreach (var folder in workspace.Folders)
            {
                imageIndex = GetImageIndex(Constants.FOLDER, folder.Expanded, false);
                View.UpdateNodeState(folder.Key, imageIndex, imageIndex);
            }
        }

        /// <summary>
        /// Expands the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The node key.</param>
        private void Expand(string key)
        {
            if (key == Constants.WORKSPACE)
            {
                View.ExpandNode(Constants.WORKSPACE);
            }
            else
            {
                ICollapsible parent = workspace.HasProject(key) ? workspace.GetProject(key) : workspace.GetFolder(key);
                parent.Expand();
            }

            View.ExpandNode(key);
        }

        /// <summary>
        /// Gets the name of the workspace.
        /// </summary>
        /// <returns>The workspace name.</returns>
        private string GetWorkspaceName()
        {
            return string.IsNullOrEmpty(workspace.Name) ? StringResources.Workspace : $"{StringResources.Workspace} '{workspace.Name}'";
        }

        /// <summary>
        /// Updates the dirty folder if one exists.
        /// </summary>
        private void UpdateDirtyFolder()
        {
            foreach (var folder in workspace.Folders)
            {
                if (folder.Dirty)
                {
                    Expand(folder.ParentKey);
                    Rename(folder.Key);
                    break;
                }
            }
        }

        /// <summary>
        /// Updates the states of the view nodes to match the states of the workspace nodes.
        /// </summary>
        private void UpdateNodes()
        {
            View.ExpandNode(Constants.WORKSPACE);

            foreach (var project in workspace.Projects)
            {
                if (project.Expanded)
                {
                    View.ExpandNode(project.Key);
                }
                else
                {
                    View.CollapseNode(project.Key);
                }
            }

            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded)
                {
                    View.ExpandNode(folder.Key);
                }
                else
                {
                    View.CollapseNode(folder.Key);
                }
            }
        }

        /// <summary>
        /// Updates the <see cref="IWorkspace"/> and the <see cref="IView"/> in response to a change in the workspace state.
        /// </summary>
        /// <param name="workspace">The new <see cref="IWorkspace"/>.</param>
        private void UpdateWorkspace(IWorkspace workspace)
        {
            this.workspace = workspace;

            View.Clear();

            var enabled = false;

            if (workspace is Workspace)
            {
                CreateWorkspaceNode();
                CreateProjectNodes();
                CreateFolderNodes();
                CreateDocumentNodes();
                UpdateNodes();
                UpdateDirtyFolder();
                
                enabled = true;
            }

            UpdateCommandState(Actions.COLLAPSE, Constants.WORKSPACE, enabled);
        }
    }
}
