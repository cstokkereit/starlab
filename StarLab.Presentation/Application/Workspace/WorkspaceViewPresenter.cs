using AutoMapper;
using StarLab.Application.DataTransfer;
using StarLab.Application.Events;
using StarLab.Presentation.Model;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace
{
    internal class WorkspaceViewPresenter : FormViewPresenter<IWorkspaceView>, IWorkspaceViewPresenter, IWorkspaceController, IWorkspaceOutputPort
    {
        private IDockableViewFactory? factory;

        private IWorkspace workspace;

        public WorkspaceViewPresenter(IWorkspaceView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events)
        {
            workspace = new Presentation.Model.Workspace();
        }

        #region IMainViewPresenter Members

        public void ClearActiveDocument()
        {
            workspace.ClearActiveDocument();

            Events.Publish(new ActiveDocumentChangedEvent(workspace));
        }

        public IDockableView CreateView(string id)
        {
            if (factory == null) throw new InvalidOperationException(StringResources.MessageNotInitialised);

            IDockableView view;

            if (workspace.HasDocument(id))
            {
                view = factory.CreateView(workspace.GetDocument(id));
            }
            else
            {
                view = factory.GetView(id);
            }

            return view;
        }

        public void Initialise(IApplicationController controller, IDockableViewFactory factory)
        {
            Initialise(controller);

            this.factory = factory;

            OpenDefaultWorkspace();
        }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            View.Text = StringResources.StarLab;

            CreateFileMenu();
            CreateViewMenu();
            CreateWorkspaceMenu();
            CreateToolsMenu();
            CreateWindowMenu();
            CreateHelpMenu();

            CreateStandardToolbar();
        }

        public void SetActiveDocument(string id)
        {
            workspace.SetActiveDocument(id);

            Events.Publish(new ActiveDocumentChangedEvent(workspace));
        }

        #endregion

        #region IViewController Members

        public override void Show(IView view)
        {
            if (view is IDockableView dockable)
            {
                View.Show(dockable);
            }
            else
            {
                View.Show(view);
            }
        }

        #endregion

        #region IWorkspaceController Members

        public void CloseWorkspace()
        {
            //DetachEventHandlers();

            View.CloseAll();
        }

        public void NewWorkspace()
        {
            throw new NotImplementedException();
        }

        public void OpenWorkspace()
        {
            OpenWorkspace(string.Empty);
        }

        public void RenameDocument(string id, string name)
        {
            var interactor = UseCaseFactory.CreateRenameDocumentUseCase(this);
            var dto = Mapper.Map<IWorkspace, WorkspaceDTO>(workspace);
            interactor.Execute(dto, id, name);
        }

        public void RenameFolder(string key, string name)
        {
            var interactor = UseCaseFactory.CreateRenameFolderUseCase(this);
            var dto = Mapper.Map<IWorkspace, WorkspaceDTO>(workspace);
            interactor.Execute(dto, key, name);
        }

        public void SaveWorkspace()
        {
            workspace.UpdateLayout(View.GetLayout());
            var interactor = UseCaseFactory.CreateSaveWorkspaceUseCase(this);
            var dto = Mapper.Map<IWorkspace, WorkspaceDTO>(workspace);
            interactor.Execute(dto);
        }

        #endregion

        #region IWorkspaceOutputPort Members

        public void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(DocumentDTO dto)
        {
            var document = workspace.GetDocument(dto.ID);

            if (document.Name != dto.Name) document.Name = dto.Name;

            Events.Publish(new WorkspaceChangedEvent(workspace));
        }

        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            workspace = new Presentation.Model.Workspace(dto);

            if (!string.IsNullOrEmpty(workspace.Layout))
            {
                View.SetLayout(workspace.Layout);
            }
            //else
            //{
            //    view.CloseAll();
            //}

            Events.Publish(new WorkspaceChangedEvent(workspace));
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Creates the file menu.
        /// </summary>
        private void CreateFileMenu()
        {
            View.AddMenuItem(Constants.FILE, StringResources.File);
            View.AddMenuItem(Constants.FILE, Constants.FILE_NEW, StringResources.New + Constants.ELLIPSIS);
            View.AddMenuSeparator(Constants.FILE);
            View.AddMenuItem(Constants.FILE, Constants.FILE_OPEN, StringResources.Open);
            View.AddMenuItem(Constants.FILE_OPEN, Constants.FILE_OPEN_WORKSPACE, StringResources.Workspace + Constants.ELLIPSIS, ImageResources.OpenWorkspace, AppController.GetCommand(this, Verbs.OPEN));
            View.AddMenuSeparator(Constants.FILE);
            View.AddMenuItem(Constants.FILE, Constants.FILE_CLOSE, StringResources.Close); //, commands.GetCommand(Constants.FILE_CLOSE));
            View.AddMenuItem(Constants.FILE, Constants.FILE_CLOSE_WORKSPACE, StringResources.CloseWorkspace, ImageResources.CloseWorkspace, AppController.GetCommand(this, Verbs.CLOSE));
            View.AddMenuSeparator(Constants.FILE);
            View.AddMenuItem(Constants.FILE, Constants.FILE_SAVE_ALL, StringResources.SaveAll, ImageResources.SaveAll, AppController.GetCommand(this, Verbs.SAVE));
            View.AddMenuSeparator(Constants.FILE);
            View.AddMenuItem(Constants.FILE, Constants.FILE_PAGE_SETUP, StringResources.PageSetup + Constants.ELLIPSIS, ImageResources.PageSetup); //, AppController.GetCommand(this, Constants.FILE_PAGE_SETUP));
            View.AddMenuItem(Constants.FILE, Constants.FILE_PRINT, StringResources.Print + Constants.ELLIPSIS, ImageResources.Print); //, AppController.GetCommand(this, Constants.FILE_PRINT));
            View.AddMenuSeparator(Constants.FILE);
            View.AddMenuItem(Constants.FILE, Constants.FILE_EXIT, StringResources.Exit); //, AppController.GetCommand(this, Constants.FILE_EXIT));
        }

        /// <summary>
        /// Creates the help menu.
        /// </summary>
        private void CreateHelpMenu()
        {
            View.AddMenuItem(Constants.HELP, StringResources.Help);
            //View.AddMenuItem(Constants.HELP, Constants.HELP_VIEW_HELP, Resources.ViewHelp, AppController.GetCommand(this, Constants.VIEW_HELP));
            View.AddMenuSeparator(Constants.HELP);
            View.AddMenuItem(Constants.HELP, Constants.HELP_ABOUT, StringResources.AboutStarLab, AppController.GetShowCommand(this, Views.ABOUT));
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void CreateStandardToolbar()
        {
            View.AddToolbarButton(Constants.FILE_OPEN_WORKSPACE, StringResources.OpenWorkspace, ImageResources.OpenWorkspace, AppController.GetCommand(this, Verbs.OPEN));
            View.AddToolbarButton(Constants.FILE_SAVE_ALL, StringResources.SaveAll, ImageResources.SaveAll, AppController.GetCommand(this, Verbs.SAVE));
        }

        /// <summary>
        /// Creates the tools menu.
        /// </summary>
        private void CreateToolsMenu()
        {
            View.AddMenuItem(Constants.TOOLS, StringResources.Tools);
            View.AddMenuItem(Constants.TOOLS, Constants.TOOLS_OPTIONS, StringResources.Options, ImageResources.Settings, AppController.GetShowCommand(this, Views.OPTIONS));
        }

        /// <summary>
        /// Creates the view menu.
        /// </summary>
        private void CreateViewMenu()
        {
            View.AddMenuItem(Constants.VIEW, StringResources.View);
            View.AddMenuItem(Constants.VIEW, Constants.VIEW_WORKSPACE_EXPLORER, StringResources.WorkspaceExplorer, AppController.GetShowCommand(this, Views.WORKSPACE_EXPLORER));
        }

        /// <summary>
        /// Creates the window menu.
        /// </summary>
        private void CreateWindowMenu()
        {
            View.AddMenuItem(Constants.WINDOW, StringResources.Window);
        }

        private void CreateWorkspaceMenu()
        {
            View.AddMenuItem(Constants.WORKSPACE, StringResources.Workspace);
            //View.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_ADD_CHART, StringResources.AddChart + Constants.ELLIPSIS, commands.GetCommand(Constants.WORKSPACE_ADD_CHART));
            //View.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_ADD_TABLE, StringResources.AddTable + Constants.ELLIPSIS, ImageResources.NewTable, commands.GetCommand(Constants.WORKSPACE_ADD_TABLE));
            View.AddMenuSeparator(Constants.WORKSPACE);
            //View.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_NEW_FOLDER, StringResources.NewFolder + Constants.ELLIPSIS, ImageResources.NewFolder, commands.GetCommand(Constants.WORKSPACE_NEW_FOLDER));
        }

        private void OpenDefaultWorkspace()
        {
            // Handle any errors
            if (!string.IsNullOrEmpty(Configuration.Workspace))
            {
                OpenWorkspace(Configuration.Workspace); // TODO - Need to set the default workspace to the current workspace when the app closes
            }
        }

        private void OpenWorkspace(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                filename = View.ShowOpenFileDialog(StringResources.OpenWorkspace, StringResources.WorkspaceFileFilter);
            }

            var interactor = UseCaseFactory.CreateOpenWorkspaceUseCase(this);

            interactor.Execute(filename);
        }

        #endregion
    }
}
