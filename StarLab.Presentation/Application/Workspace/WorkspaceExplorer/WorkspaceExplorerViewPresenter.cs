using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    internal class WorkspaceExplorerViewPresenter : ChildViewPresenter<IWorkspaceExplorerView, IViewController>, IWorkspaceExplorerViewPresenter, IWorkspaceExplorerController, ISubscriber<ActiveDocumentChangedEvent>, ISubscriber<WorkspaceChangedEvent>
    {
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
            workspace = new EmptyWorkspace();
        }

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

        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                AddImages();
                CreateToolbar();
            }
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

        public void ProjectCollapsed(string key)
        {
            workspace.GetProject(key).Collapse();
        }

        public void ProjectExpanded(string key)
        {
            workspace.GetProject(key).Expand();
        }

        public void ProjectSelected(string key)
        {

        }

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

        public void RenameDocument(string id, string name)
        {
            AppController.GetWorkspaceController().RenameDocument(id, name);
        }

        public void RenameFolder(string key, string name)
        {
            AppController.GetWorkspaceController().RenameFolder(key, name);
        }

        public void RenameWorkspace(string name)
        {
            AppController.GetWorkspaceController().RenameWorkspace(name);
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
                CreateWorkspaceNode();
                CreateProjectNodes();
                CreateFolderNodes();
                CreateDocumentNodes();
                ExpandNodes();

                foreach (var folder in workspace.Folders)
                {
                    if (folder.IsNew)
                    {
                        Rename(folder.Key);
                        break;
                    }
                }

                UpdateCommandState(Actions.COLLAPSE, Constants.WORKSPACE, true);
            }
            else
            {
                UpdateCommandState(Actions.COLLAPSE, Constants.WORKSPACE, false);
            }
            
        }

        public void ViewActivated()
        {
            var key = View.GetSelectedNode();

            if (workspace.HasFolder(key))
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

            if (workspace.HasFolder(key))
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
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.RENAME_DOCUMENT, document.ID));
        }

        private void CreateDocumentNodes()
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
            manager.AddMenuItem(Constants.ADD, Constants.ADD_CHART, StringResources.Chart + Constants.ELLIPSIS, GetCommand(workspaceController, Actions.ADD_CHART, folder.Key));
            manager.AddMenuItem(Constants.ADD, Constants.ADD_TABLE, StringResources.Table + Constants.ELLIPSIS, GetCommand(workspaceController, Actions.ADD_TABLE, folder.Key));
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

        private void CreateFolderNodes()
        {
            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded)
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, indexFolderOpened, indexSelectedOpened);
                else
                    View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, indexFolderClosed, indexSelectedClosed);

                CreateFolderMenu(folder);
            }
        }

        private void CreateProjectMenu(IProject project)
        {
            var manager = View.CreateProjectMenuManager(project.Key);

            var workspaceController = AppController.GetWorkspaceController();

            manager.AddMenuItem(Constants.COLLAPSE_ALL, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.COLLAPSE, project.Key));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.ADD, StringResources.Add);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_CHART, StringResources.Chart + Constants.ELLIPSIS, GetCommand(workspaceController, Actions.ADD_CHART, project.Key));
            manager.AddMenuItem(Constants.ADD, Constants.ADD_TABLE, StringResources.Table + Constants.ELLIPSIS, GetCommand(workspaceController, Actions.ADD_TABLE, project.Key));
            manager.AddMenuItem(Constants.ADD, Constants.ADD_FOLDER, StringResources.Folder + Constants.ELLIPSIS);
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.RENAME, project.Key));
        }

        private void CreateProjectNodes()
        {
            foreach (var project in workspace.Projects)
            {
                View.AddProjectNode(project.Key, project.ParentKey, project.Name, indexWorkspace);
                CreateProjectMenu(project);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void CreateToolbar()
        {
            View.AddToolbarButton(Constants.SYNCHRONISE, StringResources.SyncWithActiveDocument, ImageResources.Synchronise, GetCommand(Actions.SYNCHRONISE));
            View.AddToolbarButton(Constants.COLLAPSE_ALL, StringResources.CollapseAll, ImageResources.CollapseAll, GetCommand(Actions.COLLAPSE, Constants.WORKSPACE));
        }

        private void CreateWorkspaceNode()
        {
            View.AddWorkspaceNode(Constants.WORKSPACE, GetWorkspaceName(), indexWorkspace);
            CreateWorkspaceMenu();
        }

        private void CreateWorkspaceMenu()
        {
            var manager = View.CreateWorkspaceMenuManager();

            manager.AddMenuItem(Constants.COLLAPSE_ALL, StringResources.CollapseAllDescendants, ImageResources.Collapse, GetCommand(Actions.COLLAPSE, Constants.WORKSPACE));
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.ADD, StringResources.Add);
            manager.AddMenuItem(Constants.ADD, Constants.ADD_PROJECT, StringResources.Project + Constants.ELLIPSIS);
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename, ImageResources.Rename, GetCommand(Actions.RENAME, Constants.WORKSPACE));
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

            foreach (var project in workspace.Projects)
            {
                if (project.Expanded) View.ExpandNode(project.Key);
            }

            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded) View.ExpandNode(folder.Key);
            }
        }

        private string GetWorkspaceName()
        {
            return string.IsNullOrEmpty(workspace.Name) ? StringResources.Workspace : $"{StringResources.Workspace} '{workspace.Name}'";
        }

        private void UpdateView()
        {
            foreach (var project in workspace.Projects)
            {
                if (!project.Expanded) View.CollapseNode(project.Key);
            }

            foreach (var folder in workspace.Folders)
            {
                if (!folder.Expanded) View.CollapseNode(folder.Key);
            }
        }
    }
}
