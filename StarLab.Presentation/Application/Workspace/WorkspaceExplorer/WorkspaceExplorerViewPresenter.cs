using AutoMapper;
using Microsoft.VisualBasic;
using StarLab.Application.Events;
using StarLab.Commands;
using StarLab.Presentation.Model;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class WorkspaceExplorerViewPresenter : ControlViewPresenter<IWorkspaceExplorerView>, IWorkspaceExplorerViewPresenter, IWorkspaceExplorerController, ISubscriber<ActiveDocumentChangedEvent>, ISubscriber<WorkspaceChangedEvent>
    {
        private const string WORKSPACE_NAME = "{0} '{1}'";

        private IWorkspace workspace;

        private int indexFolderClosed;

        private int indexFolderOpened;

        private int indexHRDiagram;

        private int indexSelectedClosed;

        private int indexSelectedOpened;

        private int indexWorkspace;

        public WorkspaceExplorerViewPresenter(IWorkspaceExplorerView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events)
        {
            workspace = new Presentation.Model.Workspace();
        }

        #region IWorkspaceExplorerViewController Members

        public void CollapseAll()
        {
            foreach (var folder in workspace.Folders)
            {
                folder.Collapse();
            }

            UpdateView();
        }

        public void OpenDocument(string id)
        {
            if (AppController.GetWorkspaceController() is IViewController controller)
            {
                var command = AppController.GetShowCommand(controller, id);
                command?.Execute();
            }
        }

        public void Rename(string document)
        {
            View.EditNodeLabel(document);
        }

        public void Synchronise()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWorkspaceExplorerViewPresenter Members

        public void DocumentSelected(string name)
        {
            DeselectFolders();
        }

        public void FolderCollapsed(string key)
        {
            workspace.GetFolder(key).Collapse();
        }

        public void FolderExpanded(string key)
        {
            workspace.GetFolder(key).Expand();
        }

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

        public int GetImageIndex(string nodeType, bool expanded, bool selected)
        {
            int index = indexWorkspace;

            if (nodeType == Constants.FOLDER)
            {
                if (selected)
                    index = expanded ? indexSelectedOpened : indexSelectedClosed;
                else
                    index = expanded ? indexFolderOpened : indexFolderClosed;
            }

            return index;
        }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            AddImages();
            CreateWorkspaceMenu();
            CreateToolbar();

            Events.Subsribe(this);
        }

        public void RenameDocument(string id, string name)
        {
            var controller = AppController.GetWorkspaceController();
            controller?.RenameDocument(id, name);
        }

        public void RenameFolder(string key, string name)
        {
            var controller = AppController.GetWorkspaceController();
            controller?.RenameFolder(key, name);
        }

        public void ViewActivated()
        {
            var key = View.GetSelectedNode();

            if (!string.IsNullOrEmpty(key) && key != Constants.WORKSPACE && !workspace.HasDocument(key))
            {
                var folder = workspace.GetFolder(key);

                if (folder.Expanded)
                {
                    View.UpdateNodeState(folder.Key, indexFolderOpened, indexSelectedOpened);
                }
                else
                {
                    View.UpdateNodeState(folder.Key, indexFolderClosed, indexSelectedClosed);
                }
            }
        }

        public void ViewDeactivated()
        {
            var key = View.GetSelectedNode();

            if (!string.IsNullOrEmpty(key) && key != Constants.WORKSPACE && !workspace.HasDocument(key))
            {
                var folder = workspace.GetFolder(key);

                if (folder.Expanded)
                {
                    View.UpdateNodeState(folder.Key, indexFolderOpened, indexFolderOpened);
                }
                else
                {
                    View.UpdateNodeState(folder.Key, indexFolderClosed, indexFolderClosed);
                }
            }
        }

        public void WorkspaceCollapsed()
        {
            workspace.Collapse();
        }

        public void WorkspaceExpanded()
        {
            workspace.Expand();
        }

        public void WorkspaceSelected()
        {
            DeselectFolders();
        }

        #endregion

        #region Subscribed Events

        public void OnEvent(ActiveDocumentChangedEvent args)
        {
            if(GetCommand(Constants.SYNCHRONISE) is IComponentCommand command) command.Enabled = args.Workspace.ActiveDocument != null;
        }

        public void OnEvent(WorkspaceChangedEvent args)
        {
            workspace = args.Workspace;

            View.Clear();

            CreateFolders();
            CreateDocuments();
            ExpandNodes();
        }

        #endregion

        private void AddImages()
        {
            indexWorkspace = View.AddImage(ImageResources.Workspace);
            indexFolderClosed = View.AddImage(ImageResources.FolderClosed);
            indexFolderOpened = View.AddImage(ImageResources.FolderOpened);
            indexSelectedClosed = View.AddImage(ImageResources.SelectedFolderClosed);
            indexSelectedOpened = View.AddImage(ImageResources.SelectedFolderOpened);
            indexHRDiagram = View.AddImage(ImageResources.ColourMagnitudeDiagram);
        }

        private void AddToolbarButton(string name, string tooltip, Image image, string verb, bool saveCommand)
        {
            var command = AppController.GetCommand(this, verb);

            if (saveCommand) SaveCommand(name, command);

            View.AddToolbarButton(name, tooltip, image, command);
        }

        private void CreateDocumentMenu(IDocument document)
        {
            var manager = View.CreateDocumentMenuManager(document.ID);

            manager.AddMenuItem(Constants.OPEN, StringResources.Open, ImageResources.Open, AppController.GetCommand(this, Verbs.OPEN, document.ID));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete);
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, AppController.GetCommand(this, Verbs.RENAME, document.ID));
        }

        private void CreateDocuments()
        {
            foreach (var document in workspace.Documents)
            {
                View.AddDocumentNode(document.ID, document.Path, document.Name, indexHRDiagram);
                CreateDocumentMenu(document);
            }
        }

        private void CreateFolderMenu(IFolder folder)
        {
            var manager = View.CreateFolderMenuManager(folder.Key);

            manager.AddMenuItem(Constants.ADD, StringResources.Add);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_CHART, StringResources.Chart + Constants.ELLIPSIS);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_TABLE, StringResources.Table + Constants.ELLIPSIS);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_FOLDER, StringResources.NewFolder, ImageResources.NewFolder);
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.COLLAPSE_ALL, StringResources.CollapseAllDescendants, ImageResources.Collapse);
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete);
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, AppController.GetCommand(this, Verbs.RENAME, folder.Key));
        }

        private void CreateFolders()
        {
            View.AddRootNode(Constants.WORKSPACE, GetFolderName(), indexWorkspace);

            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded)
                {
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, indexFolderOpened, indexSelectedOpened);
                }
                else
                {
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, indexFolderClosed, indexSelectedClosed);
                }

                CreateFolderMenu(folder);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void CreateToolbar()
        {
            AddToolbarButton(Constants.SYNCHRONISE, StringResources.SyncWithActiveDocument, ImageResources.Synchronise, Verbs.SYNCHRONISE, true);
            AddToolbarButton(Constants.COLLAPSE_ALL, StringResources.CollapseAll, ImageResources.CollapseAll, Verbs.COLLAPSE_ALL, false);
        }

        private void CreateWorkspaceMenu()
        {
            var manager = View.CreateWorkspaceMenuManager();
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename);
        }

        private void DeselectFolders()
        {
            int imageIndex;

            foreach (var folder in workspace.Folders)
            {
                imageIndex = GetImageIndex(Constants.FOLDER, folder.Expanded, false);
                View.UpdateNodeState(folder.Key, imageIndex, imageIndex);
            }
        }

        private void ExpandNodes()
        {
            View.ExpandNode(Constants.WORKSPACE);

            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded)
                {
                    View.ExpandNode(folder.Key);
                }
            }
        }

        private string GetFolderName()
        {
            return string.IsNullOrEmpty(workspace.Name) ? StringResources.Workspace : string.Format(WORKSPACE_NAME, StringResources.Workspace, workspace.Name);
        }

        private void UpdateView()
        {
            foreach (var folder in workspace.Folders)
            {
                if (!folder.Expanded) View.CollapseNode(folder.Key);
            }
        }
    }
}
