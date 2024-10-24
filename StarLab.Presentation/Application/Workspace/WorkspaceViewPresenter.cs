﻿using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;
using System.ComponentModel;

using ImageResources = StarLab.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace
{
    internal class WorkspaceViewPresenter : Presenter, IWorkspaceViewPresenter, IWorkspaceController, IWorkspaceOutputPort, ISubscriber<ActiveDocumentChangedEvent>
    {
        private readonly IWorkspaceView view;

        private IDockableViewFactory? factory;

        private IWorkspace workspace;

        private bool confirmExit = true;

        public WorkspaceViewPresenter(IWorkspaceView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            workspace = new Workspace();

            this.view = view;
        }

        public override string Name => Views.WORKSPACE + Constants.CONTROLLER;

        public void ClearActiveDocument()
        {
            workspace.ClearActiveDocument();

            Events.Publish(new ActiveDocumentChangedEvent(workspace));
        }

        public void CloseActiveDocument()
        {
            view.CloseActiveDocument();
        }

        public void CloseWorkspace()
        {
            //DetachEventHandlers();

            view.CloseAll();

            Events.Publish(new WorkspaceClosedEvent(workspace));
        }

        public IDockableView CreateView(string id)
        {
            if (factory == null) throw new InvalidOperationException(StringResources.ObjectNotInitialised);

            IDockableView view;

            if (workspace.HasDocument(id))
                view = factory.CreateView(workspace.GetDocument(id));
            else
                view = factory.GetView(id);

            return view;
        }

        public void DeleteDocument(string id)
        {
            var interactor = UseCaseFactory.CreateDeleteDocumentUseCase(this);
            var dto = Mapper.Map<IWorkspace, WorkspaceDTO>(workspace);
            interactor.Execute(dto, id);
        }

        public void DeleteFolder(string path)
        {
            var interactor = UseCaseFactory.CreateDeleteFolderUseCase(this);
            var dto = Mapper.Map<IWorkspace, WorkspaceDTO>(workspace);
            interactor.Execute(dto, path);
        }

        public void Exit()
        {
            confirmExit = false;
            
            view.Close();
        }

        public void Initialise(IApplicationController controller, IDockableViewFactory factory)
        {
            base.Initialise(controller);

            this.factory = factory;

            CreateFileMenu();
            CreateViewMenu();
            CreateWorkspaceMenu();
            CreateToolsMenu();
            CreateWindowMenu();
            CreateHelpMenu();

            CreateStandardToolbar();

            OpenDefaultWorkspace();
        }

        public override void Initialise(IApplicationController controller)
        {
            throw new NotImplementedException(); // This should not be called
        }

        public void AddFolder(string path)
        {
            var interactor = UseCaseFactory.CreateAddFolderUseCase(this);
            var dto = Mapper.Map<IWorkspace, WorkspaceDTO>(workspace);
            interactor.Execute(dto, path);
        }

        public void NewWorkspace()
        {
            throw new NotImplementedException();
        }

        public void OnEvent(ActiveDocumentChangedEvent args)
        {
            if (GetCommand(Actions.CLOSE_DOCUMENT) is IComponentCommand command) command.Enabled = args.Workspace.ActiveDocument != null;
        }

        public void OpenDocument(string id)
        {

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
            workspace.UpdateLayout(view.GetLayout());
            var interactor = UseCaseFactory.CreateSaveWorkspaceUseCase(this);
            var dto = Mapper.Map<IWorkspace, WorkspaceDTO>(workspace);
            interactor.Execute(dto);
        }

        public void SetActiveDocument(string id)
        {
            workspace.SetActiveDocument(id);

            Events.Publish(new ActiveDocumentChangedEvent(workspace));
        }

        public void Show(IView view)
        {
            if (view is IDockableView dockable)
            {
                this.view.Show(dockable);
            }
            else
            {
                this.view.Show(view);
            }
        }

        public void ShowErrorMessage(string message)
        {
            ShowMessage(StringResources.StarLab, message, MessageBoxIcon.Error);
        }

        public void ShowWarningMessage(string message)
        {
            ShowMessage(StringResources.StarLab, message, MessageBoxIcon.Warning);
        }

        public DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return view.ShowMessage(caption, message, buttons, icon);
        }

        public void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            view.ShowMessage(caption, message, icon);
        }

        public string ShowOpenFileDialog(string title, string filter)
        {
            return view.ShowOpenFileDialog(title, filter);
        }

        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return view.ShowSaveFileDialog(title, filter, extension);
        }

        public void UpdateDocument(DocumentDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ID))
            {
                var document = workspace.GetDocument(dto.ID);

                if (!string.IsNullOrEmpty(dto.Name) && document.Name != dto.Name)
                {
                    document.Name = dto.Name;

                    Events.Publish(new WorkspaceChangedEvent(workspace));
                }
            }
        }

        public void UpdateFolders(WorkspaceDTO dto)
        {
            workspace = new Workspace(dto);

            Events.Publish(new WorkspaceChangedEvent(workspace));
        }

        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            // TODO - Change this so that it only applies changes and does not always rebuild the workspace as this causes flicker

            view.CloseAll();

            workspace = new Workspace(dto);

            if (!string.IsNullOrEmpty(workspace.Layout))
            {
                view.SetLayout(workspace.Layout);
            }

            Events.Publish(new WorkspaceChangedEvent(workspace));

            if (!string.IsNullOrEmpty(dto.FileName))
            {
                Configuration.Workspace = dto.FileName;
                Configuration.Save();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void ViewClosing(CancelEventArgs e)
        {
            if (confirmExit)
            {
                GetCommand(Actions.EXIT_APPLICATION).Execute();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Creates the file menu.
        /// </summary>
        private void CreateFileMenu()
        {
            view.AddMenuItem(Constants.FILE, StringResources.File);
            view.AddMenuItem(Constants.FILE, Constants.FILE_NEW, StringResources.New + Constants.ELLIPSIS);
            view.AddMenuSeparator(Constants.FILE);
            view.AddMenuItem(Constants.FILE, Constants.FILE_OPEN, StringResources.Open);
            view.AddMenuItem(Constants.FILE_OPEN, Constants.FILE_OPEN_WORKSPACE, StringResources.Workspace + Constants.ELLIPSIS, ImageResources.OpenWorkspace, GetCommand(Actions.OPEN_WORKSPACE));
            view.AddMenuSeparator(Constants.FILE);
            view.AddMenuItem(Constants.FILE, Constants.FILE_CLOSE, StringResources.Close, GetCommand(Actions.CLOSE_DOCUMENT));
            view.AddMenuItem(Constants.FILE, Constants.FILE_CLOSE_WORKSPACE, StringResources.CloseWorkspace, ImageResources.CloseWorkspace, GetCommand(Actions.CLOSE_WORKSPACE));
            view.AddMenuSeparator(Constants.FILE);
            view.AddMenuItem(Constants.FILE, Constants.FILE_SAVE_ALL, StringResources.SaveAll, ImageResources.SaveAll, GetCommand(Actions.SAVE_WORKSPACE));
            view.AddMenuSeparator(Constants.FILE);
            view.AddMenuItem(Constants.FILE, Constants.FILE_PAGE_SETUP, StringResources.PageSetup + Constants.ELLIPSIS, ImageResources.PageSetup); //, AppController.GetCommand(this, Constants.FILE_PAGE_SETUP));
            view.AddMenuItem(Constants.FILE, Constants.FILE_PRINT, StringResources.Print + Constants.ELLIPSIS, ImageResources.Print); //, AppController.GetCommand(this, Constants.FILE_PRINT));
            view.AddMenuSeparator(Constants.FILE);
            view.AddMenuItem(Constants.FILE, Constants.FILE_EXIT, StringResources.Exit, GetCommand(AppController, Actions.EXIT_APPLICATION));
        }

        /// <summary>
        /// Creates the help menu.
        /// </summary>
        private void CreateHelpMenu()
        {
            view.AddMenuItem(Constants.HELP, StringResources.Help);
            //view.AddMenuItem(Constants.HELP, Constants.HELP_VIEW_HELP, Resources.ViewHelp, AppController.GetCommand(this, Constants.VIEW_HELP));
            view.AddMenuSeparator(Constants.HELP);
            view.AddMenuItem(Constants.HELP, Constants.HELP_ABOUT, StringResources.AboutStarLab, GetShowViewCommand(Views.ABOUT));
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void CreateStandardToolbar()
        {
            view.AddToolbarButton(Constants.FILE_OPEN_WORKSPACE, StringResources.OpenWorkspace, ImageResources.OpenWorkspace, GetCommand(Actions.OPEN_WORKSPACE));
            view.AddToolbarButton(Constants.FILE_SAVE_ALL, StringResources.SaveAll, ImageResources.SaveAll, GetCommand(Actions.SAVE_WORKSPACE));
        }

        /// <summary>
        /// Creates the tools menu.
        /// </summary>
        private void CreateToolsMenu()
        {
            view.AddMenuItem(Constants.TOOLS, StringResources.Tools);
            view.AddMenuItem(Constants.TOOLS, Constants.TOOLS_OPTIONS, StringResources.Options, ImageResources.Settings, GetShowViewCommand(Views.OPTIONS));
        }

        /// <summary>
        /// Creates the view menu.
        /// </summary>
        private void CreateViewMenu()
        {
            view.AddMenuItem(Constants.VIEW, StringResources.View);
            view.AddMenuItem(Constants.VIEW, Constants.VIEW_WORKSPACE_EXPLORER, StringResources.WorkspaceExplorer, GetShowViewCommand(Views.WORKSPACE_EXPLORER));
        }

        /// <summary>
        /// Creates the window menu.
        /// </summary>
        private void CreateWindowMenu()
        {
            view.AddMenuItem(Constants.WINDOW, StringResources.Window);
        }

        private void CreateWorkspaceMenu()
        {
            view.AddMenuItem(Constants.WORKSPACE, StringResources.Workspace);
            //view.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_ADD_CHART, StringResources.AddChart + Constants.ELLIPSIS, commands.GetCommand(Constants.WORKSPACE_ADD_CHART));
            //view.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_ADD_TABLE, StringResources.AddTable + Constants.ELLIPSIS, ImageResources.NewTable, commands.GetCommand(Constants.WORKSPACE_ADD_TABLE));
            view.AddMenuSeparator(Constants.WORKSPACE);
            //view.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_NEW_FOLDER, StringResources.NewFolder + Constants.ELLIPSIS, ImageResources.NewFolder, commands.GetCommand(Constants.WORKSPACE_NEW_FOLDER));
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
                filename = view.ShowOpenFileDialog(StringResources.OpenWorkspace, StringResources.WorkspaceFileFilter);
            }

            var interactor = UseCaseFactory.CreateOpenWorkspaceUseCase(this);

            interactor.Execute(filename);
        }
    }
}
