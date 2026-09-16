using log4net;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents;
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
            Database,
            Folder,
            Project,
            Table,
            Workspace
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(WorkspaceExplorerViewPresenter)); // The logger that will be used for writing log messages.

        private readonly Dictionary<NodeImages, int> images = new Dictionary<NodeImages, int>(); // A dictionary that holds the node image indices.

        private readonly IWorkspaceExplorerUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        private IWorkspace workspace; // The workspace that the view represents.

        private bool copy; // true if the clipboard contains copied data; false otherwise.

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
        /// Adds a folder with the specified parent folder.
        /// </summary>
        /// <param name="key">The key that identifies the parent folder.</param>
        public void AddFolder(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            try
            {
                useCaseService.AddFolder(workspace, key);
            }
            catch (Exception e)
            {
                ShowErrorMessage(MessageBuilder.FolderCouldNotBeAdded(key));

                log.Error(e.Message, e);
            }
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
            View.Clipboard.Clear();
        }

        /// <summary>
        /// Collapses the workspace, project or folder node with the specified key.
        /// </summary>
        /// <param name="key">The workspace, project or folder key.</param>
        public void Collapse(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            if (key.Equals(Constants.Workspace))
            {
                workspace.CollapseAll();
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
        /// Initiates a clipboard copy operation.
        /// </summary>
        /// <param name="source">The key that identifies the document or folder to be copied.</param>
        public void Copy(string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source, nameof(source));

            View.Clipboard.SetText(source);

            copy = true;
        }

        /// <summary>
        /// Creates a context menu for the specified database node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="project">The database path.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateDatabaseContextMenu(string database, IMenuManager manager)
        {
            // Do Nothing
        }

        /// <summary>
        /// Creates a context menu for the specified document node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateDocumentContextMenu(string id, IMenuManager manager)
        {
            ArgumentNullException.ThrowIfNull(manager, nameof(manager));
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            
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
            ArgumentException.ThrowIfNullOrEmpty(folder, nameof(folder));
            ArgumentNullException.ThrowIfNull(manager, nameof(manager));
            
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddChart, StringResources.Chart + Constants.Ellipsis, ImageResources.NewChart, CreateCommand(GetCommandName(Actions.AddChart, folder), () => AddDocument(folder, DocumentTypes.Chart)));
            manager.AddMenuItem(Constants.Add, Constants.AddTable, StringResources.Table + Constants.Ellipsis, ImageResources.NewTable, CreateCommand(GetCommandName(Actions.AddTable, folder), () => AddDocument(folder, DocumentTypes.Table)));
            manager.AddMenuItem(Constants.Add, Constants.AddFolder, StringResources.NewFolder, ImageResources.NewFolder, CreateCommand(GetCommandName(Actions.AddFolder, folder), () => AddFolder(folder)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, CreateCommand(GetCommandName(Actions.Collapse, folder), () => Collapse(folder)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut, CreateCommand(GetCommandName(Actions.Cut, folder), () => Cut(folder)));
            manager.AddMenuItem(Constants.Copy, StringResources.Copy, ImageResources.Copy, CreateCommand(GetCommandName(Actions.Copy, folder), () => Copy(folder)));
            manager.AddMenuItem(Constants.Paste, StringResources.Paste, ImageResources.Paste, CreateCommand(GetCommandName(Actions.Paste, folder), () => Paste(folder)));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, CreateCommand(GetCommandName(Actions.Delete, folder), () => DeleteFolder(folder)));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, CreateCommand(GetCommandName(Actions.Rename, folder), () => RenameFolder(folder)));

            UpdateCommandState(GetCommandName(Actions.Paste, folder), !View.Clipboard.IsEmpty);
        }

        /// <summary>
        /// Creates a context menu for the specified project node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="project">The project name.</param>
        /// <param name="manager">The context menu manager.</param>
        public void CreateProjectContextMenu(string project, IMenuManager manager)
        {
            ArgumentException.ThrowIfNullOrEmpty(project, nameof(project));
            ArgumentNullException.ThrowIfNull(manager, nameof(manager));

            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddChart, StringResources.Chart + Constants.Ellipsis, ImageResources.NewChart, CreateCommand(Actions.AddChart, () => AddDocument(project, DocumentTypes.Chart)));
            manager.AddMenuItem(Constants.Add, Constants.AddTable, StringResources.Table + Constants.Ellipsis, ImageResources.NewTable, CreateCommand(Actions.AddTable, () => AddDocument(project, DocumentTypes.Table)));
            manager.AddMenuItem(Constants.Add, Constants.AddFolder, StringResources.NewFolder, ImageResources.NewFolder, CreateCommand(GetCommandName(Actions.AddFolder, project), () => AddFolder(project)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, CreateCommand(GetCommandName(Actions.Collapse, project), () => Collapse(project)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Cut, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.Paste, StringResources.Paste, ImageResources.Paste, CreateCommand(GetCommandName(Actions.Paste, project), () => Paste(project)));
            manager.AddMenuItem(Constants.Delete, StringResources.Delete, CreateCommand(GetCommandName(Actions.Delete, project), () => DeleteProject(project)));
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, CreateCommand(GetCommandName(Actions.Rename, project), () => RenameProject(project)));

            UpdateCommandState(GetCommandName(Actions.Paste, project), !View.Clipboard.IsEmpty);
        }

        /// <summary>
        /// Creates a context menu for the workspace node using the <see cref="IMenuManager"/> provided.
        /// </summary>
        /// <param name="manager">The context menu manager.</param>
        public void CreateWorkspaceContextMenu(IMenuManager manager)
        {
            ArgumentNullException.ThrowIfNull(manager, nameof(manager));

            manager.AddMenuItem(Constants.CollapseAll, StringResources.CollapseAllDescendants, ImageResources.Collapse, CreateCommand(Actions.CollapseWorkspace, () => Collapse(Constants.Workspace)));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Add, StringResources.Add);
            manager.AddMenuItem(Constants.Add, Constants.AddProject, StringResources.Project + Constants.Ellipsis, CreateCommand(Actions.AddProject, () => AddProject()));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.Rename, StringResources.Rename, ImageResources.Rename, CreateCommand(Actions.RenameWorkspace, () => Rename(Constants.Workspace)));
        }

        /// <summary>
        /// Initiates a clipboard cut operation.
        /// </summary>
        /// <param name="source">The key that identifies the document or folder to be cut.</param>
        public void Cut(string source)
        {
            ArgumentException.ThrowIfNullOrEmpty(source, nameof(source));

            View.Clipboard.SetText(source);

            copy = false;
        }

        /// <summary>
        /// Deletes the document with the specified ID.
        /// </summary>
        /// <param name="id">The key that identifies the Document to be deleted.</param>
        public void DeleteDocument(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            var documentID = new DocumentID(id);

            if (workspace.HasDocument(documentID))
            {
                var document = workspace.GetDocument(documentID);

                if (ConfirmAction(MessageBuilder.DocumentDeletionWarning(document.Name)))
                {
                    try
                    {
                        useCaseService.DeleteDocument(workspace, documentID);
                    }
                    catch (Exception e)
                    {
                        ShowErrorMessage(MessageBuilder.DocumentCouldNotBeDeleted(document.Name));

                        log.Error(e.Message, e);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the specified folder.
        /// </summary>
        /// <param name="folder">The path to the folder.</param>
        public void DeleteFolder(string folder)
        {
            ArgumentException.ThrowIfNullOrEmpty(folder, nameof(folder));

            if (workspace.HasFolder(folder))
            {
                if (workspace.IsEmpty(folder) || ConfirmAction(MessageBuilder.FolderDeletionWarning(workspace.GetFolder(folder).Name)))
                {
                    try
                    {
                        useCaseService.DeleteFolder(workspace, folder);
                    }
                    catch (Exception e)
                    {
                        ShowErrorMessage(MessageBuilder.FolderCouldNotBeDeleted(folder));

                        log.Error(e.Message, e);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the specified project.
        /// </summary>
        /// <param name="project">The project to be deleted.</param>
        public void DeleteProject(string project)
        {
            ArgumentException.ThrowIfNullOrEmpty(project, nameof(project));

            if (workspace.HasProject(project))
            {
                var name = workspace.GetProject(project).Name;

                if (workspace.IsEmpty(project) || ConfirmAction(MessageBuilder.ProjectDeletionWarning(name)))
                {
                    try
                    {
                        useCaseService.DeleteFolder(workspace, project);
                    }
                    catch (Exception e)
                    {
                        ShowErrorMessage(MessageBuilder.ProjectCouldNotBeDeleted(name));

                        log.Error(e.Message, e);
                    }
                }
            }
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
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            workspace.GetFolder(key).Collapse();
        }

        /// <summary>
        /// Notifies the presenter that the specified folder node has been expanded.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void FolderExpanded(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            workspace.GetFolder(key).Expand();
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            ArgumentNullException.ThrowIfNull(controller, nameof(controller));

            if (Initialised) throw new InvalidOperationException(ExceptionMessages.PresenterAlreadyInitialised(GetType()));
            
            base.Initialise(controller);

            AddImages();
            CreateToolbar();
            
            log.Debug(LogEntries.PresenterInitialised(GetType()));
        }

        /// <summary>
        /// Event handler for the ActiveDocumentChangedEvent event.
        /// </summary>
        /// <param name="args">An <see cref="ActiveDocumentChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(ActiveDocumentChangedEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(args, nameof(args));

            UpdateCommandState(Actions.Synchronise, args.Workspace.ActiveDocument != null);
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(args, nameof(args));

            UpdateWorkspace(args.Workspace);
        }

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void OpenDocument(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            AppController.ShowDocument(workspace.GetDocument(new DocumentID(key)));
        }

        /// <summary>
        /// Pastes a document or folder to the specified location.
        /// </summary>
        /// <param name="destination">The key that identifies the destination document or folder.</param>
        public void Paste(string destination)
        {
            ArgumentException.ThrowIfNullOrEmpty(destination, nameof(destination));

            if (copy)
            {
                DoCopyAndPaste(destination);
            }
            else
            {
                DoCutAndPaste(destination);
            }
        }

        /// <summary>
        /// Notifies the presenter that the specified project node has been collapsed.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void ProjectCollapsed(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            workspace.GetProject(key).Collapse();
        }

        /// <summary>
        /// Notifies the presenter that the specified project node has been expanded.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void ProjectExpanded(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            workspace.GetProject(key).Expand();
        }

        /// <summary>
        /// Renames the specified node in the workspace hierarchy.
        /// </summary>
        /// <param name="key">The key of the node to be renamed.</param>
        public void Rename(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

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
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var id = new DocumentID(key);

            try
            {
                useCaseService.RenameDocument(workspace, id, name);
            }
            catch (DocumentExistsException)
            {
                ShowErrorMessage(MessageBuilder.DocumentCouldNotBeRenamed(workspace.GetDocument(id).Name, name));
            }
            catch (InvalidNameException)
            {
                ShowErrorMessage(MessageBuilder.DocumentNameInvalid(name));
            }
        }

        /// <summary>
        /// Renames the specified folder node.
        /// </summary>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        public void RenameFolder(string key, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            try
            {
                useCaseService.RenameFolder(workspace, key, name);
            }
            catch (InvalidOperationException)
            {
                ShowErrorMessage(MessageBuilder.FolderCouldNotBeRenamed(key.Substring(key.LastIndexOf('/') + 1), name));
            }
            catch (InvalidNameException)
            {
                ShowErrorMessage(MessageBuilder.FolderNameInvalid(name));
            }
        }

        /// <summary>
        /// Renames the specified folder node.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void RenameFolder(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

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
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

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
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            try
            {
                useCaseService.RenameWorkspace(workspace, name);
            }
            catch (InvalidOperationException)
            {
                ShowErrorMessage(MessageBuilder.WorkspaceCouldNotBeRenamed(workspace.Name, name));
            }
            catch (InvalidNameException)
            {
                ShowErrorMessage(MessageBuilder.WorkspaceNameInvalid(name));
            }
            catch (Exception e)
            {
                ShowErrorMessage(MessageBuilder.WorkspaceCouldNotBeRenamed());

                log.Error(e.Message, e);
            }   
        }

        /// <summary>
        /// Makes the folder with the specified node key the current folder.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void SetSelectedFolder(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            if (key.EndsWith(Constants.Database))
            {
                var project = key.Substring(0, key.Length - (Constants.Database.Length + 1));

                if (workspace.HasProject(project)) key = project;
            }

            workspace.SetSelectedFolder(key);
        }

        /// <summary>
        /// Displays a message box with the specified error message.
        /// </summary>
        /// <param name="message">The message text.</param>
        public void ShowErrorMessage(string message)
        {
            ShowMessage(message, InteractionType.Error, InteractionResponses.OK);
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
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));
            
            AppController.GetOutputPort<IApplicationOutputPort>().UpdateDocument(dto, id);
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            AppController.GetOutputPort<IApplicationOutputPort>().UpdateWorkspace(dto);
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
        /// Adds the specified document type to the specified workspace folder.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <param name="type">A <see cref="DocumentTypes"/> that specifies the type of document being added.</param>
        private void AddDocument(string path, DocumentTypes type)
        {
            AppController.ShowAddDocumentDialog(workspace, path, type);
        }

        /// <summary>
        /// Adds the images that represent the various node types and their states.
        /// </summary>
        private void AddImages()
        {
            images.Add(NodeImages.ColourMagnitudeDiagram, View.AddImage(ImageResources.ColourMagnitudeDiagram16X16));
            images.Add(NodeImages.Database, View.AddImage(ImageResources.Database));
            images.Add(NodeImages.Folder, View.AddImage(ImageResources.Folder));
            images.Add(NodeImages.Project, View.AddImage(ImageResources.Project));
            images.Add(NodeImages.Table, View.AddImage(ImageResources.Table16X16));
            images.Add(NodeImages.Workspace, View.AddImage(ImageResources.Workspace));
        }

        /// <summary>
        /// Creates the nodes that represent the documents contained within the workspace hierarchy.
        /// </summary>
        private void CreateDocumentNodes()
        {
            foreach (var document in workspace.Documents)
            {
                switch (document.Type)
                {
                    case DocumentTypes.Chart:
                        View.AddDocumentNode(document.ID.ToString(), document.Path, document.Name, images[NodeImages.ColourMagnitudeDiagram]);
                        break;

                    case DocumentTypes.Table:
                        View.AddDocumentNode(document.ID.ToString(), document.Path, document.Name, images[NodeImages.Table]);
                        break;
                }
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
                View.AddDatabaseNode($"{project.Key}/{Constants.Database}" , project.Key, project.Database.Name, images[NodeImages.Database]);
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
        /// Performs the copy and paste operation.
        /// </summary>
        /// <param name="destination">The destination key.</param>
        private void DoCopyAndPaste(string destination)
        {
            try
            {
                useCaseService.CopyAndPaste(workspace, View.Clipboard.GetText(), destination);
            }
            catch (Exception e)
            {
                ShowErrorMessage(MessageBuilder.ClipboardContentsCouldNotBePasted(destination));

                log.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Performs the cut and paste operation in the event of a <see cref="DocumentExistsException"/> having been thrown.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="destination">The destination key.</param>
        private void DoCutAndPaste(DocumentExistsException exception, string destination)
        {
            var id = new DocumentID(View.Clipboard.GetText());

            var document = workspace.GetDocument(id);

            if (destination == document.Path)
            {
                ShowErrorMessage(MessageBuilder.DestinationSameAsSource(document.Name));
            }
            else
            {
                var result = ShowMessage(MessageBuilder.DocumentAlreadyExistsWithReplaceOption(exception.Name), InteractionType.Error, InteractionResponses.YesNoCancel);

                switch (result)
                {
                    case InteractionResult.Yes:
                        useCaseService.CutAndPaste(workspace, exception.ID, exception.Path, true);
                        break;

                    case InteractionResult.Cancel:
                        View.Clipboard.Clear();
                        break;

                    case InteractionResult.No:
                        // Do Nothing
                        break;
                }
            }
        }

        /// <summary>
        /// Performs the cut and paste operation in the event of a <see cref="DocumentExistsException"/> having been thrown.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="destination">The destination key.</param>
        private void DoCutAndPaste(FolderExistsException exception, string destination)
        {
            var folder = workspace.GetFolder(exception.DestinationFolder);

            if (exception.DestinationFolder == exception.SourceFolder)
            {
                ShowErrorMessage(MessageBuilder.DestinationSameAsSource(exception.DestinationFolder));
            }
            else
            {
                var result = ShowMessage(MessageBuilder.FolderAlreadyExists(folder.Name), InteractionType.Error, InteractionResponses.YesNoCancel);

                switch (result)
                {
                    case InteractionResult.Yes:
                        useCaseService.CutAndPaste(workspace, exception.SourceFolder, exception.DestinationFolder, true);
                        break;

                    case InteractionResult.Cancel:
                        View.Clipboard.Clear();
                        break;

                    case InteractionResult.No:
                        // Do Nothing
                        break;
                }
            }
        }

        /// <summary>
        /// Performs the cut and paste operation.
        /// </summary>
        /// <param name="destination">The destination key.</param>
        private void DoCutAndPaste(string destination)
        {
            try
            {
                useCaseService.CutAndPaste(workspace, View.Clipboard.GetText(), destination);
            }
            catch (DocumentExistsException e1)
            {
                DoCutAndPaste(e1, destination);
            }
            catch (FolderExistsException e2)
            {
                DoCutAndPaste(e2, destination);
            }
            catch (Exception e3)
            {
                ShowErrorMessage(MessageBuilder.ClipboardContentsCouldNotBePasted(destination));

                log.Error(e3.Message, e3);
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
                workspace.ExpandAll();
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

                View.SelectNode(workspace.ActiveDocument.ID.ToString());
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
