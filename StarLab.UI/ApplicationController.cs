using Castle.Windsor;
using log4net;
using StarLab.Application;
using StarLab.Presentation;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared;
using StarLab.Shared.Properties;
using StarLab.UI.Controls;
using StarLab.UI.Workspace;
using Stratosoft.Commands;
using System.Diagnostics;

using Constants = StarLab.Presentation.Constants;

namespace StarLab.UI
{
    // https://github.com/ScottPlot/ScottPlot/blob/main/src/ScottPlot5/ScottPlot5%20Demos/ScottPlot5%20WinForms%20Demo/Demos/DraggableAxisLines.cs

    // https://www.youtube.com/watch?v=280HyyLF-wU

    /// <summary>
    /// A controller that creates, initialises and manages the views that comprise the user interface of the application.
    /// </summary>
    public class ApplicationController : Controller, IApplicationController, ISubscriber<ActiveViewChangedEventArgs>, ISubscriber<WorkspaceClosedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ApplicationController)); // The logger that will be used for writing log messages.

        private readonly IWindsorContainer container; // Used to resolve dependencies at run time.

        private readonly Dictionary<ControllerID, IViewController> controllers = new Dictionary<ControllerID, IViewController>(); // A dictionary containing the view controllers indexed by ID.

        private readonly Dictionary<ViewID, IView> views = new Dictionary<ViewID, IView>(); // A dictionary containing the views indexed by ID.

        private readonly IPresenterFactory presenterFactory; // A factory for creating presenters.

        private readonly IViewFactory viewFactory; // A factory for creating views.

        private ControllerID application; // The ID of the application view controller.

        private IView? view; // The currently active view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to resolve dependencies.</param>
        /// <param name="viewFactory">An <see cref="IViewFactory"/> that will be used to create the views.</param>
        /// <param name="presenterFactory">An <see cref="IPresenterFactory"/> that will be used to create the presenters.</param>
        /// <param name="events">An <see cref="IEventAggregator"> that can be used for subscribing to and publishing events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ApplicationController(IWindsorContainer container, IViewFactory viewFactory, IPresenterFactory presenterFactory, IServiceRegistry useCaseManager, IEventAggregator events)
            : base(events)
        {
            this.presenterFactory = presenterFactory ?? throw new ArgumentNullException(nameof(presenterFactory));
            this.viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
            this.container = container ?? throw new ArgumentNullException(nameof(container));

            ID = new ControllerID(Constants.ApplicationController);

            useCaseManager.Initialise(this);
        }

        /// <summary>
        /// Gets the controller ID.
        /// </summary>
        public override ControllerID ID { get; }

        /// <summary>
        /// Closes the view that represents the specified <see cref="IDocument"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> to close.</param>
        public void CloseDocument(IDocument document)
        {
            try
            {
                var controller = GetController(document);

                controller.Close();

                log.Debug(CreateLogEntry(LogEntries.ViewClosed, document));
            }
            catch (Exception e)
            {
                log.Error(CreateLogEntry(LogEntries.ViewNotClosed, document), e);
            }
        }

        /// <summary>
        /// Deletes the <see cref="IView"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IView"/> to delete.</param>
        public void DeleteView(ViewID id)
        {
            if (views.TryGetValue(id, out IView? view))
            {
                var controllerId = new ControllerID(view);

                if (controllers[controllerId] is IDocumentController controller)
                {
                    controllers.Remove(controllerId);
                    controller.Close();
                    controller.Dispose();
                }

                views.Remove(id);
            }
        }

        /// <summary>
        /// Releases all resources used by the presenter object.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void Exit()
        {
            if (controllers[new ControllerID(ViewIDs.Application)] is IApplicationViewController controller)
            {
                controller.Exit();
            }

            foreach (var viewController in controllers.Values)
            {
                viewController.Dispose();
            }

            controllers.Clear();
            views.Clear();
        }

        /// <summary>
        /// Gets the specified <see cref="IDocumentController"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that identifies the required controller.</param>
        /// <returns>The specified <see cref="IDocumentController"/>.</returns>
        public IDocumentController GetController(IDocument document)
        {
            if (views.TryGetValue(new ViewID(document), out IView? view))
            {
                if (controllers.TryGetValue(new ControllerID(view), out IViewController? controller))
                {
                    if (controller is IDocumentController required) return required;
                }

                throw new KeyNotFoundException(string.Format(Resources.ControllerNotFound, document.ID));
            }

            throw new KeyNotFoundException(string.Format(Resources.ViewNotFound, document.ID)); 
        }



        /// <summary>
        /// Gets the specified output port.
        /// </summary>
        /// <typeparam name="TOutputPort">The type of output port required.</typeparam>
        /// <param name="id">The ID of the parent controller.</param>
        /// <returns>The specified output port.</returns>
        /// <exception cref="Exception"></exception>
        public TOutputPort GetOutputPort<TOutputPort>(ControllerID id)
        {
            if (controllers.TryGetValue(id, out IViewController? parent))
            {
                if (parent is TOutputPort parentPort) return parentPort;

                foreach (var child in parent.ChildControllers)
                {
                    if (child is TOutputPort childPort) return childPort;
                }
            }

            throw new Exception(string.Format(Resources.UnknownType, typeof(TOutputPort)));
        }

        /// <summary>
        /// Gets the specified output port.
        /// </summary>
        /// <typeparam name="TOutputPort">The type of output port required.</typeparam>
        /// <returns>The specified output port.</returns>
        /// <exception cref="Exception"></exception>
        public TOutputPort GetOutputPort<TOutputPort>()
        {
            foreach (var controller in controllers.Values)
            {
                if (controller is TOutputPort) return (TOutputPort)controller;

                foreach (var child in controller.ChildControllers)
                {
                    if (child is TOutputPort) return (TOutputPort)child;
                }
            }

            throw new Exception(string.Format(Resources.UnknownType, typeof(TOutputPort)));
        }

        /// <summary>
        /// Gets the <see cref="IView"/> specified by the <see cref="IDocument"/> provided. If the view does not already exist it will be created.
        /// </summary>
        /// <param name="document">An instance of <see cref="IDocument"/> that specifies which instance of <see cref="IView"/> is required.</param>
        /// <returns>The required <see cref="IView">.</returns>
        public IView GetView(IDocument document)
        {
            var id = new ViewID(document);

            if (!views.ContainsKey(id))
            {
                CreateDocumentView(document);
            }

            return views[id];
        }

        /// <summary>
        /// Gets the <see cref="IView"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IView"/>.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView GetView(ViewID id) 
        {
            if (views.TryGetValue(id, out IView? view))
            {
                return view;
            }

            throw new ArgumentException(string.Format(Resources.ViewNotFound, id), nameof(id));
        }

        /// <summary>
        /// Event handler for the ActiveViewChanged event.
        /// </summary>
        /// <param name="args">An <see cref="ActiveViewChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(ActiveViewChangedEventArgs args)
        {
            if (view != null && args.View != null)
            {
                log.Debug(string.Format(LogEntries.ActiveViewChanged, view.ID, args.View.ID));
            }

            view = args.View;
        }

        /// <summary>
        /// Event handler for the WorkspaceClosedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceClosedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceClosedEventArgs args)
        {
            foreach (var document in args.Workspace.Documents)
            {
                var viewID = new ViewID(document);
                var controllerId = new ControllerID(views[viewID]);

                var controller = controllers[controllerId];
                
                controller.Dispose();

                controllers.Remove(controllerId);

                views.Remove(viewID);
            }
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Run()
        {
            Events.Subsribe(this);

            CreateViews();

            try
            {
                var application = CreateApplicationView();

                log.Debug(string.Format(LogEntries.ViewCreated, ViewIDs.Application));

                System.Windows.Forms.Application.Run(application);
            }
            catch (Exception e)
            {
                log.Fatal(e.Message, e);
            }
        }

        /// <summary>
        /// Displays the About dialog.
        /// </summary>
        public void ShowAboutDialog()
        {
            Show(ViewIDs.About);
        }

        /// <summary>
        /// Displays the Add Chart dialog.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> to which the new chart document will be added.</param>
        /// <param name="path">The path to the folder.</param>
        public void ShowAddChartDialog(IWorkspace workspace, string path)
        {
            var view = GetView(ViewIDs.AddDocument);

            var controller = GetController(view);

            controller.Run(new AddDocumentWorkflowContext(path, DocumentTypes.Chart));

            controllers[application].Show(view);
        }

        /// <summary>
        /// Displays a Document window that contains the <see cref="IDocument"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> to show.</param>
        public void ShowDocument(IDocument document)
        {
            var id = new ViewID(document);

            if (!views.ContainsKey(id))
            {
                CreateDocumentView(document);
            }

            Show(id);
        }

        /// <summary>
        /// Displays a message box with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
           return GetActiveController().ShowMessage(caption, message, type, responses);
        }

        /// <summary>
        /// Displays a message box with the specified caption, message and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            return ShowMessage(caption, message, InteractionType.Info, responses);
        }

        /// <summary>
        /// Displays a message box with the specified caption and message.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        public void ShowMessage(string caption, string message)
        {
            ShowMessage(caption, message, InteractionResponses.OK);
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return GetActiveController().ShowOpenFileDialog(title, filter);
        }

        /// <summary>
        /// Displays the Options dialog.
        /// </summary>
        public void ShowOptionsDialog()
        {
            Show(ViewIDs.Options);
        }

        /// <summary>
        /// Displays a save file dialog with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return GetActiveController().ShowSaveFileDialog(title, filter, extension);
        }

        /// <summary>
        /// Displays the Workspace Explorer.
        /// </summary>
        public void ShowWorkspaceExplorer()
        {
            Show(ViewIDs.WorkspaceExplorer);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="AboutViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Nothing to do
            }
        }

        /// <summary>
        /// Creates the main application window. Can only be called after all of the other views have been created.
        /// </summary>
        private ApplicationView CreateApplicationView()
        {
            var commands = CreateCommands();

            var view = viewFactory.CreateApplicationView(Resources.StarLab);

            var presenter = presenterFactory.CreatePresenter(view, commands);

            if (presenter is IViewController controller)
            {
                application = controller.ID;
                controllers.Add(application, controller);
                controller.Initialise(this);
            }

            return (ApplicationView)view;
        }

        /// <summary>
        /// Creates the command manager and registers the command invokers used to execute commands in response to events raised bu user interface elements.
        /// </summary>
        /// <returns>The required <see cref="ICommandManager"/>.</returns>
        private ICommandManager CreateCommands()
        {
            var commands = container.Resolve<ICommandManager>();

            commands.RegisterCommandInvoker(new ToolStripMenuItemCommandInvoker());
            commands.RegisterCommandInvoker(new ToolStripButtonCommandInvoker());
            commands.RegisterCommandInvoker(new ButtonCommandInvoker());

            return commands;
        }

        /// <summary>
        /// Creates the specified dialog view.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        private void CreateDialogView(string name, string text)
        {
            try
            {
                var commands = CreateCommands();

                var childView = viewFactory.CreateChildView(name, name);
                var childPresenter = presenterFactory.CreatePresenter(childView, commands);

                var view = viewFactory.CreateDialogView(name, text, childView);
                var presenter = presenterFactory.CreatePresenter(view, childPresenter, commands);

                if (presenter is IViewController controller)
                {
                    controllers.Add(controller.ID, controller);
                    controller.Initialise(this);
                }

                log.Debug(string.Format(LogEntries.ViewCreated, view.Name));

                views.Add(view.ID, view);
            }
            catch (Exception e)
            {
                log.Error(string.Format(LogEntries.ViewNotCreated, name), e);
            }
        }

        /// <summary>
        /// Creates all of the dialog views.
        /// </summary>
        private void CreateDialogViews()
        {
            CreateDialogView(ViewNames.About, Resources.AboutStarLab);
            CreateDialogView(ViewNames.AddDocument, Resources.AddDocument);
            CreateDialogView(ViewNames.Options, Resources.Options);
        }

        /// <summary>
        /// Creates the specified document view.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that .</param>
        private void CreateDocumentView(IDocument document)
        {
            try
            {
                var commands = CreateCommands();

                var childViews = viewFactory.CreateChildViews(document);
                var childPresenters = presenterFactory.CreatePresenters(document, childViews, commands);

                var view = viewFactory.CreateView(document, childViews);
                var presenter = presenterFactory.CreatePresenter(document, view, childPresenters, commands);

                if (presenter is IViewController controller)
                {
                    controllers.Add(controller.ID, controller);
                    controller.Initialise(this);
                }

                log.Debug(CreateLogEntry(LogEntries.ViewCreated, document));

                views.Add(view.ID, view);
            }
            catch (Exception e)
            {
                log.Error(CreateLogEntry(LogEntries.ViewNotCreated, document), e);
            }
        }

        /// <summary>
        /// Creates the specified tool view.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        private void CreateToolView(string name, string text)
        {
            try
            {
                var commands = CreateCommands();

                var childView = viewFactory.CreateChildView(name, name);
                var childPresenter = presenterFactory.CreatePresenter(childView, commands);

                var view = viewFactory.CreateToolView(name, text, childView);
                var presenter = presenterFactory.CreatePresenter(view, childPresenter, commands);

                if (presenter is IViewController controller)
                {
                    controllers.Add(controller.ID, controller);
                    controller.Initialise(this);
                }

                log.Debug(string.Format(LogEntries.ViewCreated, view.Name));

                views.Add(view.ID, view);
            }
            catch (Exception e)
            {
                log.Error(string.Format(LogEntries.ViewNotCreated, name), e);
            }
        }

        /// <summary>
        /// Creates all of the tool views.
        /// </summary>
        private void CreateToolViews()
        {
            CreateToolView(ViewNames.WorkspaceExplorer, Resources.WorkspaceExplorer);
        }

        /// <summary>
        /// Creates all of the fixed views. This excludes any views that implement <see cref="IDocumentView"/> as these are created in response to a workspace being loaded or modified.
        /// </summary>
        private void CreateViews()
        {
            CreateDialogViews();
            CreateToolViews();
        }

        /// <summary>
        /// Creates the specified log entry from the information in the <see cref="IDocument"/> provided.
        /// </summary>
        /// <param name="template">The log entry template.</param>
        /// <param name="document">The <see cref="IDocument"/> that is the subject of the log entry.</param>
        /// <returns>The specified log entry.</returns>
        private string CreateLogEntry(string template, IDocument document)
        {
            string message = string.Empty;

            if (document is IChartDocument)
            {
                message = string.Format(template, $"chart {document.Name} ({document.ID})");
            }

            Debug.Assert(!string.IsNullOrEmpty(message));

            return message;
        }

        /// <summary>
        /// Gets the <see cref="IViewController"/> that controls the active view. If there is no currently active view the application view controller will be returned.
        /// </summary>
        /// <returns>The <see cref="IViewController"/> that controls the active view.</returns>
        private IViewController GetActiveController()
        {
            if (view != null && controllers.TryGetValue(new ControllerID(view), out IViewController? controller))
            {
                return controller;
            }
            
            return controllers[new ControllerID(ViewIDs.Application)];
        }

        /// <summary>
        /// Gets the specified <see cref="IDialogController"/>.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IDialogController"/>.</param>
        /// <returns>The specified <see cref="IDialogController"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        private IDialogController GetController(IView view)
        {
            var id = new ControllerID(view);

            if (controllers.TryGetValue(id, out IViewController? controller))
            {
                if (controller is IDialogController required) return required;
            }

            throw new KeyNotFoundException(string.Format(Resources.ControllerNotFound, id));
        }

        /// <summary>
        /// Displays the <see cref="IView"/> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the view to be shown.</param>
        /// <exception cref="ArgumentException"></exception>
        private void Show(ViewID id)
        {
            if (views.TryGetValue(id, out IView? view))
            {
                controllers[application].Show(view);
            }
            else
            {
                throw new ArgumentException(string.Format(Resources.ViewNotFound, id), nameof(id));
            }
        }
    }
}
