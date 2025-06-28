using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IWorkspaceExplorerView"/>.
    /// </summary>
    internal class WorkspaceExplorerViewPresenter : ChildViewPresenter<IWorkspaceExplorerView, IViewController>, IWorkspaceExplorerViewPresenter, IWorkspaceExplorerController, IWorkspaceOutputPort, ISubscriber<ActiveDocumentChangedEventArgs>, ISubscriber<WorkspaceChangedEventArgs>
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
        /// <param name="configuration">The <see cref="IApplicationConfiguration"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public WorkspaceExplorerViewPresenter(IWorkspaceExplorerView view, ICommandManager commands, IUseCaseFactory factory, IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events)
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
            if (AppController.GetView(Views.AddDocument) is IDialog dialog)
            {
                dialog.Show(new AddDocumentInteractionContext(workspace, path, DocumentType.Chart));
            }
        }

        /// <summary>
        /// Adds a folder with the specified parent folder.
        /// </summary>
        /// <param name="key">The key that identifies the parent folder.</param>
        public void AddFolder(string key)
        {
            var interactor = UseCaseFactory.CreateAddFolderUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key);
        }

        /// <summary>
        /// Displays the Add Project dialog.
        /// </summary>
        public void AddProject()
        {
            throw new NotImplementedException();
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
            var interactor = UseCaseFactory.CreateUseCase(this, ClipboardOperations.Copy);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key);
        }

        /// <summary>
        /// Creates a context menu for the specified document node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateDocumentContextMenu(string id, IMenuManager manager)
        {
            manager.AddMenuItem(Constants.Open, StringResources.Open, ImageResources.Open, GetCommand(Actions.OpenDocument, id));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut, GetCommand(Actions.Cut, id));
            manager.AddMenuItem(Constants.Copy, StringResources.Copy, ImageResources.Copy, GetCommand(Actions.Copy, id));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, GetCommand(Actions.DeleteDocument, id));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.Rename, id));
        }

        /// <summary>
        /// Creates a context menu for the specified folder node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="folder">The folder path.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateFolderContextMenu(string folder, IMenuManager manager)
        {
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddChart, StringResources.Chart + Constants.Ellipsis, GetCommand(Actions.AddChart, folder));
            manager.AddMenuItem(Constants.Add, Constants.AddTable, StringResources.Table + Constants.Ellipsis, GetCommand(Actions.AddTable, folder));
            manager.AddMenuItem(Constants.Add, Constants.AddFolder, StringResources.NewFolder, ImageResources.NewFolder, GetCommand(Actions.AddFolder, folder));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.Collapse, folder));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut, GetCommand(Actions.Cut, folder));
            manager.AddMenuItem(Constants.Copy, StringResources.Copy, ImageResources.Copy, GetCommand(Actions.Copy, folder));
            manager.AddMenuItem(Constants.Paste, StringResources.Paste, ImageResources.Paste, GetCommand(Actions.Paste, folder));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, GetCommand(Actions.DeleteFolder, folder));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.Rename, folder));

            // TODO - Need to update the state of the cut, copy and paste commands to reflect the currently available operations
            //UpdateCommandState(Actions.Paste, Constants.Workspace, clipboard.GetDataFormat() == "Workspace.Folder");
        }

        /// <summary>
        /// Creates a context menu for the specified project node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="project">The project name.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateProjectContextMenu(string project, IMenuManager manager)
        {
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddChart, StringResources.Chart + Constants.Ellipsis, GetCommand(Actions.AddChart, project));
            manager.AddMenuItem(Constants.Add, Constants.AddTable, StringResources.Table + Constants.Ellipsis, GetCommand(Actions.AddTable, project));
            manager.AddMenuItem(Constants.Add, Constants.AddFolder, StringResources.NewFolder, ImageResources.NewFolder, GetCommand(Actions.AddFolder, project));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.Collapse, project));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.Paste, StringResources.Paste, ImageResources.Paste, GetCommand(Actions.Paste, project));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, GetCommand(Actions.DeleteFolder, project));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.Rename, project));
        }

        /// <summary>
        /// Creates a context menu for the workspace node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="manager">The context menu manager.</param>
        public void CreateWorkspaceContextMenu(IMenuManager manager)
        {
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.Collapse, Constants.Workspace));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddProject, StringResources.Project + Constants.Ellipsis, GetCommand(Actions.AddProject));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.Rename, Constants.Workspace));
        }

        /// <summary>
        /// Cuts the specified document or folder.
        /// </summary>
        /// <param name="key">The key that identifies the document or folder to be cut.</param>
        public void Cut(string key)
        {
            var interactor = UseCaseFactory.CreateUseCase(this, ClipboardOperations.Cut);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key);
        }

        /// <summary>
        /// Deletes the document with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the document to be deleted.</param>
        public void DeleteDocument(string id)
        {
            var interactor = UseCaseFactory.CreateDeleteDocumentUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, id);
        }

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="key">The key that identifies the folder to be deleted.</param>
        public void DeleteFolder(string key)
        {
            var interactor = UseCaseFactory.CreateDeleteFolderUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key);
        }

        /// <summary>
        /// Deletes the specified project.
        /// </summary>
        /// <param name="key">The key that identifies the project to be deleted.</param>
        public void DeleteProject(string key)
        {
            var interactor = UseCaseFactory.CreateDeleteFolderUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key);
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
                    imageIndex = GetImageIndex(Constants.Folder, folder.Expanded, true);
                    View.UpdateNodeState(folder.Key, imageIndex, imageIndex);
                }
                else
                {
                    imageIndex = GetImageIndex(Constants.Folder, folder.Expanded, false);
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

            if (nodeType == Constants.Folder)
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
            var document = workspace.GetDocument(key);
            var view = AppController.GetView(document);
            AppController.Show(view);
        }

        /// <summary>
        /// Pastes a document or folder to the specified location.
        /// </summary>
        /// <param name="key">The key that identifies the destination for the document or folder.</param>
        public void Paste(string key)
        {
            var interactor = UseCaseFactory.CreateUseCase(this, ClipboardOperations.Paste);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key);
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
            DeselectFolders();
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
            var interactor = UseCaseFactory.CreateRenameDocumentUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key, name);
        }

        /// <summary>
        /// Renames the specified folder node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        public void RenameFolder(string key, string name)
        {
            var interactor = UseCaseFactory.CreateRenameFolderUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, key, name);
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
        /// Renames the workspace.
        /// </summary>
        /// <param name="name">The new name.</param>
        public void RenameWorkspace(string name)
        {
            var interactor = UseCaseFactory.CreateRenameWorkspaceUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto, name);
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
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        /// <param name="id">The ID of the document that was modified.</param>
        public void UpdateDocument(WorkspaceDTO dto, string id)
        {
            if (AppController.GetController(ControllerNames.ApplicationViewController) is IApplicationOutputPort port) port.UpdateDocument(dto, id);
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            if (AppController.GetController(ControllerNames.ApplicationViewController) is IApplicationOutputPort port) port.UpdateWorkspace(dto);
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
                if (folder.Expanded)
                {
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, images[NodeImages.OpenFolder], images[NodeImages.SelectedOpenFolder]);
                }
                else
                {
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, images[NodeImages.ClosedFolder], images[NodeImages.SelectedClosedFolder]);
                }
            }
        }

        /// <summary>
        /// Creates the nodes that represent the projects contained within the workspace hierarchy.
        /// </summary>
        private void CreateProjectNodes()
        {
            foreach (var project in workspace.Projects)
            {
                View.AddProjectNode(project.Key, project.ParentKey, project.Name, images[NodeImages.Workspace]);
            }
        }

        /// <summary>
        /// Creates the Workspace Explorer toolbar.
        /// </summary>
        private void CreateToolbar()
        {
            View.AddToolbarButton(Constants.Synchronise, StringResources.SyncWithActiveDocument, ImageResources.Synchronise, GetCommand(Actions.Synchronise));
            View.AddToolbarButton(Constants.CollapseAll, StringResources.CollapseAll, ImageResources.CollapseAll, GetCommand(Actions.Collapse, Constants.Workspace));
        }

        /// <summary>
        /// Creates the node that represents the workspace.
        /// </summary>
        private void CreateWorkspaceNode()
        {
            View.AddWorkspaceNode(Constants.Workspace, GetWorkspaceName(), images[NodeImages.Workspace]);
        }

        /// <summary>
        /// Deselects the folders.
        /// </summary>
        private void DeselectFolders()
        {
            int imageIndex;

            foreach (var folder in workspace.Folders)
            {
                imageIndex = GetImageIndex(Constants.Folder, folder.Expanded, false);
                View.UpdateNodeState(folder.Key, imageIndex, imageIndex);
            }
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

            UpdateCommandState(Actions.Collapse, Constants.Workspace, enabled);
        }
    }
}
