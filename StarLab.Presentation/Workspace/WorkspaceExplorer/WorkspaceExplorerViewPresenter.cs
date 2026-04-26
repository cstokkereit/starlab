using log4net;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Configuration;
using StarLab.Shared;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// Controls the behaviour of the workspace explorer tool.
    /// </summary>
    internal class WorkspaceExplorerViewPresenter : ChildViewPresenter<IWorkspaceExplorerView, IViewController>, IWorkspaceExplorerViewPresenter, IWorkspaceExplorerController, IWorkspaceOutputPort, ISubscriber<ActiveDocumentChangedEventArgs>, ISubscriber<WorkspaceChangedEventArgs>
    {
        /// <summary>
        /// Provides constants that can be used to access the node image indices.
        /// </summary>
        private enum NodeImages
        {
            ColourMagnitudeDiagram,
            Folder,
            Project,
            Workspace
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(WorkspaceExplorerViewPresenter)); // The logger that will be used for writing log messages.

        private readonly Dictionary<NodeImages, int> images = new Dictionary<NodeImages, int>(); // A dictionary that holds the node image indices.

        private readonly IWorkspaceExplorerUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        private string clipboard = string.Empty; // The key that identifies the current contents of the clipboard.

        private IWorkspace workspace; // The workspace that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceExplorerViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IWorkspaceExplorerView"/> controlled by this presenter.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public WorkspaceExplorerViewPresenter(IWorkspaceExplorerView view, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            useCaseService = services.GetService<IWorkspaceExplorerUseCaseService>();

            view.Attach(this);

            workspace = new EmptyWorkspace();
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~WorkspaceExplorerViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Adds a chart to the specified workspace folder.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        public void AddChart(string path)
        {
            AppController.ShowAddChartDialog(workspace, path);
        }

        /// <summary>
        /// Adds a folder with the specified parent folder.
        /// </summary>
        /// <param name="key">The key that identifies the parent folder.</param>
        public void AddFolder(string key)
        {
            useCaseService.AddFolder(workspace, key);
        }

        /// <summary>
        /// Displays the Add Project dialog.
        /// </summary>
        public void AddProject()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a table to the specified workspace folder.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        public void AddTable(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the clipboard.
        /// </summary>
        public void ClearClipboard()
        {
            clipboard = string.Empty;
        }

        /// <summary>
        /// Collapses the workspace, project or folder node with the specified key.
        /// </summary>
        /// <param name="key">The workspace, project or folder key.</param>
        public void Collapse(string key)
        {
            if (key.Equals(Constants.Workspace))
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
        /// Copies the specified document or folder.
        /// </summary>
        /// <param name="key">The key that identifies the document or folder to be copied.</param>
        public void Copy(string key)
        {
            useCaseService.Copy(workspace, key);
        }

        /// <summary>
        /// Creates a context menu for the specified document node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateDocumentContextMenu(string id, IMenuManager manager)
        {
            manager.AddMenuItem(Constants.Open, StringResources.Open, ImageResources.Open, CreateCommand(GetCommandName(Actions.Open, id), () => OpenDocument(id)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut, CreateCommand(GetCommandName(Actions.Cut, id), () => Cut(id)));
            manager.AddMenuItem(Constants.Copy, StringResources.Copy, ImageResources.Copy, CreateCommand(GetCommandName(Actions.Copy, id), () => Copy(id)));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, CreateCommand(GetCommandName(Actions.Delete, id), () => DeleteDocument(id)));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, CreateCommand(GetCommandName(Actions.Rename, id), () => Rename(id)));
        }

        /// <summary>
        /// Creates a context menu for the specified folder node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="folder">The folder path.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateFolderContextMenu(string folder, IMenuManager manager)
        {
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddChart, StringResources.Chart + Constants.Ellipsis, CreateCommand(GetCommandName(Actions.AddChart, folder), () => AddChart(folder)));
            //manager.AddMenuItem(Constants.Add, Constants.AddTable, StringResources.Table + Constants.Ellipsis, CreateCommand(GetCommandName(Actions.AddTable, folder), () => AddTable(folder)));
            manager.AddMenuItem(Constants.Add, Constants.AddFolder, StringResources.NewFolder, ImageResources.NewFolder, CreateCommand(GetCommandName(Actions.AddFolder, folder), () => AddFolder(folder)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, CreateCommand(GetCommandName(Actions.Collapse, folder), () => Collapse(folder)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut, CreateCommand(GetCommandName(Actions.Cut, folder), () => Cut(folder)));
            manager.AddMenuItem(Constants.Copy, StringResources.Copy, ImageResources.Copy, CreateCommand(GetCommandName(Actions.Copy, folder), () => Copy(folder)));
            manager.AddMenuItem(Constants.Paste, StringResources.Paste, ImageResources.Paste, CreateCommand(GetCommandName(Actions.Paste, folder), () => Paste(folder)));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, CreateCommand(GetCommandName(Actions.Delete, folder), () => DeleteFolder(folder)));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, CreateCommand(GetCommandName(Actions.Rename, folder), () => RenameFolder(folder)));

            UpdateCommandState(GetCommandName(Actions.Paste, folder), !string.IsNullOrEmpty(clipboard));
        }

        /// <summary>
        /// Creates a context menu for the specified project node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="project">The project name.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateProjectContextMenu(string project, IMenuManager manager)
        {
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddChart, StringResources.Chart + Constants.Ellipsis, CreateCommand(Actions.AddChart, () => AddChart(project)));
            manager.AddMenuItem(Constants.Add, Constants.AddTable, StringResources.Table + Constants.Ellipsis, CreateCommand(Actions.AddTable, () => AddTable(project)));
            manager.AddMenuItem(Constants.Add, Constants.AddFolder, StringResources.NewFolder, ImageResources.NewFolder, CreateCommand(GetCommandName(Actions.AddFolder, project), () => AddFolder(project)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, CreateCommand(GetCommandName(Actions.Collapse, project), () => Collapse(project)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.Paste, StringResources.Paste, ImageResources.Paste, CreateCommand(GetCommandName(Actions.Paste, project), () => Paste(project)));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, CreateCommand(GetCommandName(Actions.Delete, project), () => DeleteProject(project)));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, CreateCommand(GetCommandName(Actions.Rename, project), () => RenameProject(project)));

            UpdateCommandState(GetCommandName(Actions.Paste, project), !string.IsNullOrEmpty(clipboard));
        }

        /// <summary>
        /// Creates a context menu for the workspace node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="manager">The context menu manager.</param>
        public void CreateWorkspaceContextMenu(IMenuManager manager)
        {
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, CreateCommand(Actions.CollapseWorkspace, () => Collapse(Constants.Workspace)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddProject, StringResources.Project + Constants.Ellipsis, CreateCommand(Actions.AddProject, () => AddProject()));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, CreateCommand(Actions.RenameWorkspace, () => Rename(Constants.Workspace)));
        }

        /// <summary>
        /// Cuts the specified document or folder.
        /// </summary>
        /// <param name="key">The key that identifies the document or folder to be cut.</param>
        public void Cut(string key)
        {
            useCaseService.Cut(workspace, key);
        }

        /// <summary>
        /// Deletes the document with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the document to be deleted.</param>
        public void DeleteDocument(string id)
        {
            useCaseService.DeleteDocument(workspace, id);
        }

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="key">The key that identifies the folder to be deleted.</param>
        public void DeleteFolder(string key)
        {
            useCaseService.DeleteFolder(workspace, key);
        }

        /// <summary>
        /// Deletes the specified project.
        /// </summary>
        /// <param name="key">The key that identifies the project to be deleted.</param>
        public void DeleteProject(string key)
        {
            useCaseService.DeleteFolder(workspace, key);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="WorkspaceExplorerViewPresenter"/> object.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
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
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (Initialised) throw new InvalidOperationException(string.Format(StringResources.AlreadyInitialised, nameof(WorkspaceExplorerViewPresenter)));
            
            base.Initialise(controller);

            AddImages();
            CreateToolbar();
            
            log.Debug(string.Format(LogEntries.Initialised, nameof(WorkspaceExplorerViewPresenter)));
        }

        /// <summary>
        /// Event handler for the ActiveDocumentChangedEvent event.
        /// </summary>
        /// <param name="args">An <see cref="ActiveDocumentChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(ActiveDocumentChangedEventArgs args)
        {
            UpdateCommandState(Actions.Synchronise, args.Workspace.ActiveDocument != null);
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
            AppController.ShowDocument(workspace.GetDocument(key));
        }

        /// <summary>
        /// Pastes a document or folder to the specified location.
        /// </summary>
        /// <param name="key">The key that identifies the destination for the document or folder.</param>
        public void Paste(string key)
        {
            useCaseService.Paste(workspace, key);
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
        /// Renames the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The key of the node to be renamed.</param>
        public void Rename(string key)
        {
            if (key == Constants.Workspace)
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
        /// Renames the specified document node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        public void RenameDocument(string key, string name)
        {
            useCaseService.RenameDocument(workspace, key, name);
        }

        /// <summary>
        /// Renames the specified folder node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        public void RenameFolder(string key, string name)
        {
            useCaseService.RenameFolder(workspace, key, name);
        }

        /// <summary>
        /// Renames the specified folder node.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void RenameFolder(string key)
        {
            var folder = workspace.GetFolder(key);
            Expand(folder.ParentKey);
            Rename(key);
        }

        /// <summary>
        /// Renames the specified project node.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void RenameProject(string key)
        {
            var folder = workspace.GetProject(key);
            Expand(folder.ParentKey);
            Rename(key);
        }

        /// <summary>
        /// Renames the workspace.
        /// </summary>
        /// <param name="name">The new name.</param>
        public void RenameWorkspace(string name)
        {
            useCaseService.RenameWorkspace(workspace, name);
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
            UpdateSelectedNode(true);
        }

        /// <summary>
        /// Updates the contents of the clipboard.
        /// </summary>
        /// <param name="key">The key that identifies the target of the current clipboard operation.</param>
        public void UpdateClipboard(string key)
        {
            clipboard = key;
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        /// <param name="id">The ID of the document that was modified.</param>
        public void UpdateDocument(WorkspaceDTO dto, string id)
        {
            AppController.GetOutputPort<IApplicationOutputPort>().UpdateDocument(dto, id);
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            AppController.GetOutputPort<IApplicationOutputPort>().UpdateWorkspace(dto);
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
        /// Releases any resources used by the <see cref="WorkspaceExplorerViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                View.Detach();
            }
        }

        /// <summary>
        /// Adds the images that represent the various node types and their states.
        /// </summary>
        private void AddImages()
        {
            images.Add(NodeImages.ColourMagnitudeDiagram, View.AddImage(ImageResources.ColourMagnitudeDiagram16X16));
            images.Add(NodeImages.Folder, View.AddImage(ImageResources.Folder));
            images.Add(NodeImages.Project, View.AddImage(ImageResources.Project));
            images.Add(NodeImages.Workspace, View.AddImage(ImageResources.Workspace));
        }

        /// <summary>
        /// Creates the nodes that represent the documents contained within the workspace hierarchy.
        /// </summary>
        private void CreateDocumentNodes()
        {
            foreach (var document in workspace.Documents)
            {
                View.AddDocumentNode(document.ID, document.Path, document.Name, images[NodeImages.ColourMagnitudeDiagram]);
            }
        }

        /// <summary>
        /// Creates the nodes that represent the folders contained within the workspace hierarchy.
        /// </summary>
        private void CreateFolderNodes()
        {
            foreach (var folder in workspace.Folders)
            {
                View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, images[NodeImages.Folder]);
            }
        }

        /// <summary>
        /// Creates the nodes that represent the projects contained within the workspace hierarchy.
        /// </summary>
        private void CreateProjectNodes()
        {
            foreach (var project in workspace.Projects)
            {
                View.AddProjectNode(project.Key, project.ParentKey, project.Name, images[NodeImages.Project]);
            }
        }

        /// <summary>
        /// Creates the Workspace Explorer toolbar.
        /// </summary>
        private void CreateToolbar()
        {
            View.AddToolbarButton(Constants.Synchronise, StringResources.SyncWithActiveDocument, ImageResources.Synchronise, CreateCommand(Actions.Synchronise, Synchronise));
            View.AddToolbarButton(Constants.CollapseAll, StringResources.CollapseAll, ImageResources.CollapseAll, CreateCommand(Actions.CollapseWorkspace, () => Collapse(Constants.Workspace)));
        }

        /// <summary>
        /// Creates the node that represents the workspace.
        /// </summary>
        private void CreateWorkspaceNode()
        {
            View.AddWorkspaceNode(Constants.Workspace, GetWorkspaceName(), images[NodeImages.Workspace]);
        }

        /// <summary>
        /// Expands the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The node key.</param>
        private void Expand(string key)
        {
            if (key == Constants.Workspace)
            {
                View.ExpandNode(Constants.Workspace);
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
        /// Updates the states of the view nodes to match the states of the workspace nodes.
        /// </summary>
        private void UpdateNodes()
        {
            View.ExpandNode(Constants.Workspace);

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
        /// Selects the node that represents the active document.
        /// </summary>
        /// <param name="highlight">true to set the focus on the selected node; false otherwise.</param>
        private void UpdateSelectedNode(bool highlight)
        {
            if (workspace.ActiveDocument != null)
            {
                if (highlight)
                {
                    View.FocusOnSelectedNode();
                }

                View.SelectNode(workspace.ActiveDocument.ID);
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

                UpdateSelectedNode(false);

                enabled = true;
            }

            UpdateCommandState(Actions.CollapseWorkspace, enabled);
        }
    }
}
