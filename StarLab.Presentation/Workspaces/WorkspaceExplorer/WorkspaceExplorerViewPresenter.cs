using AutoMapper;
using StarLab.Application.UseCases;
using StarLab.Commands;
using StarLab.Presentation.Events;
using StarLab.Presentation.Model;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspaces.WorkspaceExplorer
{
    internal class WorkspaceExplorerViewPresenter : ControlViewPresenter<IWorkspaceExplorerView>, IWorkspaceExplorerViewPresenter, IWorkspaceExplorerController, ISubscriber<ActiveDocumentChangedEvent>, ISubscriber<WorkspaceChangedEvent>
    {
        private const string WORKSPACE_NAME = "{0} '{1}'";

        private IWorkspace workspace;

        public WorkspaceExplorerViewPresenter(IWorkspaceExplorerView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events)
        {
            workspace = new Workspace();
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

        public void Synchronise()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IWorkspaceExplorerViewPresenter Members

        public string GetImageKey(string nodeType, bool expanded)
        {
            var imageKey = string.Empty;

            switch (nodeType)
            {
                case Constants.FOLDER:
                    imageKey = expanded ? Constants.FOLDER_OPENED : Constants.FOLDER_CLOSED;
                    break;

                case Constants.WORKSPACE:
                    imageKey = Constants.WORKSPACE;
                    break;
            }

            return imageKey;
        }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            AddImages();
            CreateFolders();
            CreateDocumentMenuManager();
            CreateFolderMenuManager();
            CreateWorkspaceMenuManager();
            CreateToolbar(controller);

            Events.Subsribe(this);
        }

        #endregion

        #region Subscribed Events

        public void OnEvent(ActiveDocumentChangedEvent args)
        {
            var command = GetCommand(Constants.SYNCHRONISE);
            command.Enabled = args.Workspace.ActiveDocument != null;
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
            View.AddImage(Constants.COLOUR_MAGNITUDE_DIAGRAM, ImageResources.ColourMagnitudeDiagram);
            View.AddImage(Constants.FOLDER_CLOSED, ImageResources.FolderClosed);
            View.AddImage(Constants.FOLDER_OPENED, ImageResources.FolderOpened);
            View.AddImage(Constants.WORKSPACE, ImageResources.Workspace);
        }

        private void AddToolbarButton(IApplicationController controller, string name, string tooltip, Image image, string verb, bool saveCommand)
        {
            ICommand command = controller.GetCommand(CreateAction<IWorkspaceExplorerController>(this, verb));

            if (saveCommand) SaveCommand(name, (IComponentCommand)command);

            View.AddToolbarButton(name, tooltip, image, command);
        }

        private void CreateDocumentMenuManager()
        {
            var manager = View.CreateDocumentMenuManager();

            manager.AddMenuItem(Constants.OPEN, StringResources.Open);
            manager.AddMenuSeparator();
            manager.AddMenuItem(Constants.CUT, StringResources.Cut, ImageResources.Cut);
            manager.AddMenuItem(Constants.COPY, StringResources.Copy, ImageResources.Copy);
            manager.AddMenuItem(Constants.PASTE, StringResources.Paste, ImageResources.Paste);
            manager.AddMenuItem(Constants.DELETE, StringResources.Delete);
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename);
        }

        private void CreateDocuments()
        {
            foreach (var document in workspace.Documents)
            {
                View.AddDocumentNode(document.FullName, document.Path, document.Name, document.Type);
            }
        }

        private void CreateFolderMenuManager()
        {
            var manager = View.CreateFolderMenuManager();

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
            manager.AddMenuItem(Constants.RENAME, StringResources.Rename);
        }

        private void CreateFolders()
        {
            string imageKey;

            View.AddRootNode(Constants.WORKSPACE, GetFolderName(), Constants.WORKSPACE);

            foreach (var folder in workspace.Folders)
            {
                imageKey = folder.Expanded ? Constants.FOLDER_OPENED : Constants.FOLDER_CLOSED;

                View.AddFolderNode(folder.Key, folder.ParentKey, folder.Name, imageKey);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void CreateToolbar(IApplicationController controller)
        {
            AddToolbarButton(controller, Constants.SYNCHRONISE, StringResources.SyncWithActiveDocument, ImageResources.Synchronise, Verbs.SYNCHRONISE, true);
            AddToolbarButton(controller, Constants.COLLAPSE_ALL, StringResources.CollapseAll, ImageResources.CollapseAll, Verbs.COLLAPSE_ALL, false);
        }

        private void CreateWorkspaceMenuManager()
        {
            var manager = View.CreateWorkspaceMenuManager();

            manager.AddMenuItem(Constants.RENAME, StringResources.Rename);
        }

        private void ExpandNodes()
        {
            View.Expand(Constants.WORKSPACE);

            foreach (var folder in workspace.Folders)
            {
                if (folder.Expanded)
                {
                    View.Expand(folder.Path);
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
                if (!folder.Expanded) View.Collapse(folder.Key);
            }
        }
    }
}
