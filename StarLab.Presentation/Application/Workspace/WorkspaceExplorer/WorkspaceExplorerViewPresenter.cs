using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;

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

        public WorkspaceExplorerViewPresenter(IWorkspaceExplorerView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events)
        {
            workspace = new Workspace();
        }

        public override string Name => throw new NotImplementedException();

        public void CollapseAll()
        {
            foreach (var folder in workspace.Folders)
            {
                folder.Collapse();
            }

            UpdateView();
        }

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

        public void Initialise(IApplicationController controller, IFormController parentController)
        {
            base.Initialise(controller, parentController);

            AddImages();
            CreateWorkspaceMenu();
            CreateToolbar();
        }

        public void OnEvent(ActiveDocumentChangedEvent args)
        {
            UpdateCommandState(Actions.SYNCHRONISE, args.Workspace.ActiveDocument != null);
        }

        public void OnEvent(WorkspaceChangedEvent args)
        {
            UpdateWorkspace(args.Workspace);
        }

        public void OpenDocument(string id)
        {
            AppController.Show(id);
        }

        public void Rename(string key)
        {
            View.EditNodeLabel(key);
        }

        public void RenameDocument(string id, string name)
        {
            AppController.GetWorkspaceController().RenameDocument(id, name);
        }

        public void RenameFolder(string key, string name)
        {
            AppController.GetWorkspaceController().RenameFolder(key, name);
        }

        public void ShowErrorMessage(string message)
        {
            ShowMessage(StringResources.StarLab, message, MessageBoxIcon.Error);
        }

        public void Synchronise()
        {
            throw new NotImplementedException();
        }

        private void UpdateWorkspace(IWorkspace workspace)
        {
            this.workspace = workspace;

            View.Clear();

            if (workspace is Workspace)
            {
                CreateFolders();
                CreateDocuments();
                ExpandNodes();

                foreach (var folder in workspace.Folders)
                {
                    if (folder.IsNew)
                    {
                        Rename(folder.Key);
                        break;
                    }
                }

                UpdateCommandState(Actions.COLLAPSE_ALL, true);
            }
            else
            {
                UpdateCommandState(Actions.COLLAPSE_ALL, false);
            }
            
        }

        public void ViewActivated()
        {
            var key = View.GetSelectedNode();

            if (!string.IsNullOrEmpty(key) && key != Constants.WORKSPACE && !workspace.HasDocument(key))
            {
                var folder = workspace.GetFolder(key);

                if (folder.Expanded)
                    View.UpdateNodeState(folder.Key, indexFolderOpened, indexSelectedOpened);
                else
                    View.UpdateNodeState(folder.Key, indexFolderClosed, indexSelectedClosed);
            }
        }

        public void ViewDeactivated()
        {
            var key = View.GetSelectedNode();

            if (!string.IsNullOrEmpty(key) && key != Constants.WORKSPACE && !workspace.HasDocument(key))
            {
                var folder = workspace.GetFolder(key);

                if (folder.Expanded)
                    View.UpdateNodeState(folder.Key, indexFolderOpened, indexFolderOpened);
                else
                    View.UpdateNodeState(folder.Key, indexFolderClosed, indexFolderClosed);
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

        private void AddImages()
        {
            indexWorkspace = View.AddImage(ImageResources.Workspace);
            indexFolderClosed = View.AddImage(ImageResources.FolderClosed);
            indexFolderOpened = View.AddImage(ImageResources.FolderOpened);
            indexSelectedClosed = View.AddImage(ImageResources.SelectedFolderClosed);
            indexSelectedOpened = View.AddImage(ImageResources.SelectedFolderOpened);
            indexHRDiagram = View.AddImage(ImageResources.ColourMagnitudeDiagram);
        }

        private void CreateDocumentMenu(IDocument document)
        {
            var manager = View.CreateDocumentMenuManager(document.ID);

            var workspaceController = AppController.GetWorkspaceController();

            manager.AddMenuItem(Constants.OPEN, StringResources.Open, ImageResources.Open, GetCommand(Actions.OPEN_DOCUMENT, document.ID));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete, GetCommand(workspaceController, Actions.DELETE_DOCUMENT, document.ID));
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, GetCommand(Actions.RENAME_DOCUMENT, document.ID));
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

            var workspaceController = AppController.GetWorkspaceController();

            manager.AddMenuItem(Constants.ADD, StringResources.Add);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_CHART, StringResources.Chart + Constants.ELLIPSIS);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_TABLE, StringResources.Table + Constants.ELLIPSIS);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_FOLDER, StringResources.NewFolder, ImageResources.NewFolder, GetCommand(workspaceController, Actions.ADD_FOLDER, folder.Key));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.COLLAPSE_ALL, StringResources.CollapseAllDescendants, ImageResources.Collapse);
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete, GetCommand(workspaceController, Actions.DELETE_FOLDER, folder.Key));
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, GetCommand(Actions.RENAME_FOLDER, folder.Key));
        }

        private void CreateFolders()
        {
            View.AddRootNode(Constants.WORKSPACE, GetFolderName(), indexWorkspace);

            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded)
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, indexFolderOpened, indexSelectedOpened);
                else
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, indexFolderClosed, indexSelectedClosed);

                CreateFolderMenu(folder);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void CreateToolbar()
        {
            View.AddToolbarButton(Constants.SYNCHRONISE, StringResources.SyncWithActiveDocument, ImageResources.Synchronise, GetCommand(Actions.SYNCHRONISE));
            View.AddToolbarButton(Constants.COLLAPSE_ALL, StringResources.CollapseAll, ImageResources.CollapseAll, GetCommand(Actions.COLLAPSE_ALL));
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
                if (folder.Expanded) View.ExpandNode(folder.Key);
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
