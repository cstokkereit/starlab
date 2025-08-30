using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Workspace;
using Stratosoft.Commands;
using System.ComponentModel;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IApplicationView"/>.
    /// </summary>
    internal class ApplicationViewPresenter : Presenter, IApplicationViewPresenter, IApplicationViewController, IApplicationOutputPort, ISubscriber<ActiveDocumentChangedEventArgs>
    {
        private readonly IApplicationView view; // The view controlled by the presenter.

        private IWorkspace workspace; // The workspace that the view represents.

        private bool confirmExit = true; // A flag that, when set to true, indicates that a confirmation dialog must be shown prior to exiting the application.

        private bool dirty = false; // A flag that, when set to true, indicates that the workspace has unsaved changes.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IApplicationView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IApplicationConfiguration"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ApplicationViewPresenter(IApplicationView view, ICommandManager commands, IUseCaseFactory factory, Configuration.IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, factory, configuration, mapper, events)
        {
            workspace = new EmptyWorkspace();

            this.view = view;
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => ControllerNames.WorkspaceController;

        /// <summary>
        /// Clears the active document.
        /// </summary>
        public void ClearActiveDocument()
        {
            workspace.ClearActiveDocument();

            Events.Publish(new ActiveDocumentChangedEventArgs(workspace));
        }

        /// <summary>
        /// Closes the active document.
        /// </summary>
        public void CloseActiveDocument()
        {
            view.CloseActiveDocument();
        }

        /// <summary>
        /// Closes the workspace.
        /// </summary>
        public void CloseWorkspace()
        {
            var close = true;

            if (dirty)
            {
                var result = AppController.ShowMessage(StringResources.StarLab, StringResources.WorkspaceClosing, InteractionResponses.YesNoCancel);

                if (result == InteractionResult.Yes) SaveWorkspace();

                close = result != InteractionResult.Cancel;
            }

            if (close)
            {
                UpdateCommandState(Actions.CloseWorkspace, false);

                var layout = workspace.Layout;

                view.CloseAll();

                Events.Publish(new WorkspaceClosedEventArgs(workspace));

                workspace = new EmptyWorkspace();

                view.SetLayout(layout); // This will restore any open tool windows

                Events.Publish(new WorkspaceChangedEventArgs(workspace));
            }
        }

        /// <summary>
        /// Returns the <see cref="IDockableView"/> with the specified ID if it exists. If not, a new <see cref="IDockableView"/> with the specified ID will be created.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IDockableView"/>.</param>
        /// <returns>The <see cref="IDockableView"/> with the specified ID.</returns>
        public IDockableView? CreateView(string id)
        {
            IView? view;

            if (workspace.HasDocument(id))
            {
                view = AppController.GetView(workspace.GetDocument(id));
            }
            else
            {
                view = AppController.GetView(id);
            }

            return (IDockableView?)view;
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void Exit()
        {
            confirmExit = false;
            view.CloseAll();
            view.Close();
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

                CreateFileMenu();
                CreateViewMenu();
                CreateWorkspaceMenu();
                CreateToolsMenu();
                CreateWindowMenu();
                CreateHelpMenu();

                CreateStandardToolbar();

                OpenDefaultWorkspace();
            }
        }

        /// <summary>
        /// Creates a new workspace.
        /// </summary>
        public void NewWorkspace()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Event handler for the active document changed event.
        /// </summary>
        /// <param name="args">An <see cref="ActiveDocumentChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(ActiveDocumentChangedEventArgs args)
        {
            UpdateCommandState(Actions.CloseDocument, args.Workspace.ActiveDocument != null);
        }

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The ID of the document to be opened.</param>
        public void OpenDocument(string id)
        {
            Show(AppController.GetView(workspace.GetDocument(id)));
        }

        /// <summary>
        /// Opens a workspace.
        /// </summary>
        public void OpenWorkspace()
        {
            OpenWorkspace(string.Empty);
        }

        /// <summary>
        /// Saves the workspace.
        /// </summary>
        public void SaveWorkspace()
        {
            workspace.UpdateLayout(view.GetLayout());
            var interactor = UseCaseFactory.CreateSaveWorkspaceUseCase(this);
            var dto = Mapper.Map<WorkspaceDTO>(workspace);
            interactor.Execute(dto);
        }

        /// <summary>
        /// Makes the document with the specified ID the active document.
        /// </summary>
        /// <param name="id">The ID of the document.</param>
        public void SetActiveDocument(string id)
        {
            workspace.SetActiveDocument(id);

            Events.Publish(new ActiveDocumentChangedEventArgs(workspace));
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided and applies the layout.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        public void SetWorkspace(WorkspaceDTO dto)
        {
            workspace = new Workspace.Workspace(dto);

            view.CloseAll();

            if (!string.IsNullOrEmpty(workspace.Layout)) view.SetLayout(workspace.Layout);

            Events.Publish(new WorkspaceChangedEventArgs(workspace));

            if (!string.IsNullOrEmpty(dto.FileName) && !Configuration.Workspace.Equals(dto.FileName))
            {
                Configuration.Workspace = dto.FileName;
            }

            UpdateCommandState(Actions.CloseDocument, workspace.ActiveDocument != null);
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            if (view is IDockableView dockable)
            {
                this.view.Show(dockable);
                workspace.UpdateLayout(this.view.GetLayout());
                Events.Publish(new WorkspaceChangedEventArgs(workspace));
            }
            else
            {
                this.view.Show(view);
            }
        }

        /// <summary>
        /// Shows the <see cref="IView"> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the view to be shown.</param>
        public void Show(string id)
        {
            AppController.Show(id);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return AppController.ShowMessage(caption, message, type, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption, message and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return AppController.ShowMessage(caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption and message.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message)
        {
            return AppController.ShowMessage(caption, message);
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return view.ShowOpenFileDialog(title, filter);
        }

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return view.ShowSaveFileDialog(title, filter, extension);
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        /// <param name="id">The ID of the document that was modified.</param>
        public void UpdateDocument(WorkspaceDTO dto, string id)
        {
            workspace = new Workspace.Workspace(dto);

            var document = workspace.GetDocument(id);

            var controller = AppController.GetController(workspace.GetDocument(id));

            controller.UpdateDocument(document);

            Events.Publish(new WorkspaceChangedEventArgs(workspace));

            dirty = true;
        }

        /// <summary>
        /// Replaces the current workspace state with that specified by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that represents the new workspace state.</param>
        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            var idsOld = workspace.GetDocumentIDs();

            workspace = new Workspace.Workspace(dto);

            var idsNew = workspace.GetDocumentIDs();

            foreach (var id in idsOld)
            {
                if (!idsNew.Contains(id))
                {
                    AppController.DeleteView(id);
                }
            }

            Events.Publish(new WorkspaceChangedEventArgs(workspace), true); // Event published synchronously to allow renaming of folder in WorkspaceExplorer. Could make this a more specific event type if necessary.

            if (!string.IsNullOrEmpty(dto.FileName) && !Configuration.Workspace.Equals(dto.FileName))
            {
                Configuration.Workspace = dto.FileName;
            }

            UpdateCommandState(Actions.CloseDocument, workspace.ActiveDocument != null);

            dirty = true;
        }

        /// <summary>
        /// Notifies the presenter that the view is being closed.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs"/> that can be used to determine the reasons that the view is closing and, if necessary, cancel it.</param>
        public void ViewClosing(CancelEventArgs e)
        {
            if (confirmExit)
            {
                GetCommand(Actions.Exit).Execute();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Creates the File menu.
        /// </summary>
        private void CreateFileMenu()
        {
            view.AddMenuItem(Constants.File, StringResources.File);
            view.AddMenuItem(Constants.File, Constants.FileNew, StringResources.New);
            view.AddMenuItem(Constants.FileNew, Constants.FileNewProject, StringResources.Project + Constants.Ellipsis);
            view.AddMenuItem(Constants.FileNew, Constants.FileNewWorkspace, StringResources.Workspace + Constants.Ellipsis);
            view.AddMenuSeparator(Constants.File);
            view.AddMenuItem(Constants.File, Constants.FileOpen, StringResources.Open);
            view.AddMenuItem(Constants.FileOpen, Constants.FileOpenWorkspace, StringResources.Workspace + Constants.Ellipsis, ImageResources.OpenWorkspace, GetCommand(Actions.OpenWorkspace));
            view.AddMenuSeparator(Constants.File);
            view.AddMenuItem(Constants.File, Constants.FileClose, StringResources.Close, GetCommand(Actions.CloseDocument));
            view.AddMenuItem(Constants.File, Constants.FileCloseWorkspace, StringResources.CloseWorkspace, ImageResources.CloseWorkspace, GetCommand(Actions.CloseWorkspace));
            view.AddMenuSeparator(Constants.File);
            view.AddMenuItem(Constants.File, Constants.FileSaveAll, StringResources.SaveAll, ImageResources.SaveAll, GetCommand(Actions.SaveWorkspace));
            view.AddMenuSeparator(Constants.File);
            view.AddMenuItem(Constants.File, Constants.FilePageSetup, StringResources.PageSetup + Constants.Ellipsis, ImageResources.PageSetup); //, AppController.GetCommand(this, Constants.FILE_PAGE_SETUP));
            view.AddMenuItem(Constants.File, Constants.FilePrint, StringResources.Print + Constants.Ellipsis, ImageResources.Print); //, AppController.GetCommand(this, Constants.FILE_PRINT));
            view.AddMenuSeparator(Constants.File);
            view.AddMenuItem(Constants.File, Constants.FileExit, StringResources.Exit, GetCommand(AppController, Actions.Exit));
        }

        /// <summary>
        /// Creates the Help menu.
        /// </summary>
        private void CreateHelpMenu()
        {
            view.AddMenuItem(Constants.Help, StringResources.Help);
            //view.AddMenuItem(Constants.HELP, Constants.HELP_VIEW_HELP, Resources.ViewHelp, AppController.GetCommand(this, Constants.VIEW_HELP));
            view.AddMenuSeparator(Constants.Help);
            view.AddMenuItem(Constants.Help, Constants.HelpAbout, StringResources.AboutStarLab, GetShowViewCommand(Views.About));
        }

        /// <summary>
        /// Creates the standard toolbar.
        /// </summary>
        private void CreateStandardToolbar()
        {
            view.AddToolbarButton(Constants.FileOpenWorkspace, StringResources.OpenWorkspace, ImageResources.OpenWorkspace, GetCommand(Actions.OpenWorkspace));
            view.AddToolbarButton(Constants.FileSaveAll, StringResources.SaveAll, ImageResources.SaveAll, GetCommand(Actions.SaveWorkspace));
        }

        /// <summary>
        /// Creates the Tools menu.
        /// </summary>
        private void CreateToolsMenu()
        {
            view.AddMenuItem(Constants.Tools, StringResources.Tools);
            view.AddMenuItem(Constants.Tools, Constants.ToolsOptions, StringResources.Options, ImageResources.Settings, GetShowViewCommand(Views.Options));
        }

        /// <summary>
        /// Creates the View menu.
        /// </summary>
        private void CreateViewMenu()
        {
            view.AddMenuItem(Constants.View, StringResources.View);
            view.AddMenuItem(Constants.View, Constants.ViewWorkspaceExplorer, StringResources.WorkspaceExplorer, GetShowViewCommand(Views.WorkspaceExplorer));
        }

        /// <summary>
        /// Creates the Window menu.
        /// </summary>
        private void CreateWindowMenu()
        {
            view.AddMenuItem(Constants.Window, StringResources.Window);
        }

        /// <summary>
        /// Creates the Workspace menu.
        /// </summary>
        private void CreateWorkspaceMenu()
        {
            view.AddMenuItem(Constants.Workspace, StringResources.Workspace);
            //view.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_ADD_CHART, StringResources.AddChart + Constants.ELLIPSIS, commands.GetCommand(Constants.WORKSPACE_ADD_CHART));
            //view.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_ADD_TABLE, StringResources.AddTable + Constants.ELLIPSIS, ImageResources.NewTable, commands.GetCommand(Constants.WORKSPACE_ADD_TABLE));
            view.AddMenuSeparator(Constants.Workspace);
            //view.AddMenuItem(Constants.WORKSPACE, Constants.WORKSPACE_NEW_FOLDER, StringResources.NewFolder + Constants.ELLIPSIS, ImageResources.NewFolder, commands.GetCommand(Constants.WORKSPACE_NEW_FOLDER));
        }

        /// <summary>
        /// Opens the default workspace.
        /// </summary>
        private void OpenDefaultWorkspace()
        {
            if (!string.IsNullOrEmpty(Configuration.Workspace))
            {
                OpenWorkspace(Configuration.Workspace);
            }
        }

        /// <summary>
        /// Opens the specified workspace file. If the path to the workspace file is omitted an Open File dialog will be displayed.
        /// </summary>
        /// <param name="filename">The fully qualified path to the workspace file.</param>
        private void OpenWorkspace(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                filename = view.ShowOpenFileDialog(StringResources.OpenWorkspace, StringResources.WorkspaceFileFilter);
            }

            var interactor = UseCaseFactory.CreateOpenWorkspaceUseCase(this);

            interactor.Execute(filename);

            UpdateCommandState(Actions.CloseWorkspace, true);
        }
    }
}
