using log4net;
using StarLab.Application;
using StarLab.Presentation;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using StarLab.UI.Controls;
using StarLab.UI.Workspace.Documents;
using Stratosoft.Commands;
using System.Diagnostics;

namespace StarLab.UI
{
    // https://github.com/ScottPlot/ScottPlot/blob/main/src/ScottPlot5/ScottPlot5%20Demos/ScottPlot5%20WinForms%20Demo/Demos/DraggableAxisLines.cs

    // https://www.youtube.com/watch?v=280HyyLF-wU

    /// <summary>
    /// A controller that creates, initialises and manages the views that comprise the user interface of the application.
    /// </summary>
    public class ApplicationController : Controller, IApplicationController, ISubscriber<WorkspaceClosedEventArgs>
    {
        private readonly IDictionary<string, IViewController> controllers = new Dictionary<string, IViewController>(); // A dictionary containing the view controllers indexed by name.

        private static readonly ILog log = LogManager.GetLogger(typeof(ApplicationController)); // The logger that will be used for writing log messages.

        private readonly IDictionary<string, IView> views = new Dictionary<string, IView>(); // A dictionary containing the views indexed by ID.

        private readonly IViewFactory viewFactory; // A factory for creating views.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="viewFactory">An <see cref="IViewFactory"/> that will be used to create the views.</param>
        /// <param name="interactorFactory">An <see cref="IUseCaseFactory"> that will be used to create the use case interactors.</param>
        /// <param name="events">An <see cref="IEventAggregator"> that can be used for subscribing to and publishing events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ApplicationController(IViewFactory viewFactory, IUseCaseFactory interactorFactory, IEventAggregator events)
            : base(interactorFactory, events)
        {
            this.viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => Controllers.ApplicationController;

        /// <summary>
        /// Creates the <see cref="ICommand"> specified by the controller, action and target provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="target">The target for the action.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        public ICommand CreateCommand(ICommandManager commands, IController controller, string action, string target)
        {
            return new ActionCommand(commands, controller, action, [target]);
        }

        /// <summary>
        /// Creates the <see cref="ICommand"> specified by the controller and action provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <param name="action">The action to be performed when the <see cref="ICommand.Execute"/> method is called.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        public ICommand CreateCommand(ICommandManager commands, IController controller, string action)
        {
            return new ActionCommand(commands, controller, action);
        }

        /// <summary>
        /// Creates an <see cref="ICommand"/> that will show the specified view.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="view">The name of the <see cref="IView"/> to be shown.</param>
        /// <returns>An instance of <see cref="ICommand"/> that can be used to show the specified view.</returns>
        public ICommand CreateCommand(ICommandManager commands, string view)
        {
            return CreateCommand(commands, this, Actions.Show, view);
        }

        /// <summary>
        /// Deletes the <see cref="IView"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IView"/> to delete.</param>
        public void DeleteView(string id)
        {
            if (views.ContainsKey(id))
            {
                var name = Controllers.GetDocumentControllerName(id);

                if (controllers[name] is IDocumentController controller)
                {
                    controllers.Remove(name);
                    controller.Close();
                }

                views.Remove(id);
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void Exit()
        {
            if (controllers[Controllers.ApplicationViewController] is IApplicationViewController controller) controller.Exit();
        }

        /// <summary>
        /// Gets the <see cref="IDocumentController"/> that controls the view representing the <see cref="IDocument"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> represented by the view controlled by the <see cref="IDocumentController"/>.</param>
        /// <returns>The required <see cref="IDocumentController"/>.</returns>
        public IDocumentController GetController(IDocument document)
        {
            return (IDocumentController)GetController(GetView(document));
        }

        /// <summary>
        /// Gets the specified <see cref="IController"/>.
        /// </summary>
        /// <param name="name">The name of the controller.</param>
        /// <returns>The required <see cref="IController"/>.</returns>
        public IController GetController(string name)
        {
            return controllers[name];
        }

        /// <summary>
        /// Gets the <see cref="IView"/> specified by the <see cref="IDocument"/> provided. If the view does not already exist it will be created.
        /// </summary>
        /// <param name="document">An instance of <see cref="IDocument"/> that specifies which instance of <see cref="IView"/> is required.</param>
        /// <returns>The required <see cref="IView">.</returns>
        public IView GetView(IDocument document)
        {
            IView view;

            if (views.ContainsKey(document.ID))
            {
                view = views[document.ID];
            }
            else
            {
                view = viewFactory.CreateView(document);

                var controller = ((DocumentView)view).Controller;

                Debug.Assert(controller != null);

                controllers.Add(controller.Name, controller);
                controller.Initialise(this);

                views.Add(view.ID, view);
            }

            return view;
        }

        /// <summary>
        /// Gets the <see cref="IView"/> with the specified ID. If the view does not exist <see cref="null"/> will be returned.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IView"/>.</param>
        /// <returns>The required <see cref="IView"/> or <see cref="null"/>.</returns>
        public IView? GetView(string id)
        {
            if (views.TryGetValue(id, out IView? view))
            {
                return view;
            }

            return null;
        }

        /// <summary>
        /// Event handler for the WorkspaceClosedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceClosedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceClosedEventArgs args)
        {
            // TODO - Close workspace properly
            // Change to a custom dialog that will centre on the application
            // Perform any cleanup here prior to closing the workspace
            // Teardown parent child relationships

            foreach (var document in args.Workspace.Documents)
            {
                controllers.Remove(Controllers.GetDocumentControllerName(document.ID));
                views.Remove(document.ID);
            }
        }

        /// <summary> 
        /// Registers the available command invokers with the instance of <see cref="ICommandManager"/> provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that will be used to register the available command invokers.</param>
        public void RegisterCommandInvokers(ICommandManager commands)
        {
            commands.RegisterCommandInvoker(new ToolStripMenuItemCommandInvoker());
            commands.RegisterCommandInvoker(new ToolStripButtonCommandInvoker());
            commands.RegisterCommandInvoker(new ButtonCommandInvoker());
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Run()
        {
            Events.Subsribe(this);

            InitialiseServices();

            log.Info(Resources.InitialisationComplete);

            CreateViews();

            var view = views[Views.Application];

            if (views[Views.Application] is Form form) System.Windows.Forms.Application.Run(form);
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            var controller = GetController(view);
            controller.Initialise(this);

            controllers[Controllers.ApplicationViewController].Show(view);
        }

        /// <summary>
        /// Shows the <see cref="IView"/> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the view to be shown.</param>
        /// <exception cref="ViewNotFoundException"></exception>
        public void Show(string id)
        {
            if (!views.ContainsKey(id)) throw new ViewNotFoundException(id);

            var view = views[id];

            var controller = GetController(view);
            controller.Initialise(this);

            controllers[Controllers.ApplicationViewController].Show(view); // TODO - May need to include an overload of this that includes the parent view
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
            var controller = (IMessageBoxController)controllers[Controllers.MessageBoxController];
            return controller.ShowMessage(GetMessageBoxOwner(), caption, message, type, responses);
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
            var controller = (IMessageBoxController)controllers[Controllers.MessageBoxController];
            return controller.ShowMessage(GetMessageBoxOwner(), caption, message, responses);
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified caption and message.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message)
        {
            var controller = (IMessageBoxController)controllers[Controllers.MessageBoxController];
            return controller.ShowMessage(GetMessageBoxOwner(), caption, message);
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        private void CreateView(string name, string text)
        {
            try
            {
                var view = viewFactory.CreateView(name, text);

                IViewController? controller = view.Controller;

                Debug.Assert(controller != null);

                views.Add(view.ID, view);
                controllers.Add(controller.Name, controller);
                controller.Initialise(this);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled) log.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Creates all of the fixed views. This excludes any views that implement <see cref="IDocumentView"/> as these are created in response to a workspace being loaded or modified.
        /// </summary>
        private void CreateViews()
        {
            CreateView(Views.About, Resources.AboutStarLab);
            CreateView(Views.AddDocument, Resources.AddDocument);
            CreateView(Views.MessageBox, Resources.StarLab);
            CreateView(Views.Options, Resources.Options);
            CreateView(Views.WorkspaceExplorer, Resources.WorkspaceExplorer);

            // NOTE - This must be the last view to be created.
            CreateView(Views.Application, Resources.StarLab);
        }

        /// <summary>
        /// Gets the <see cref="IViewController"/> that controls the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that is controlled by the <see cref="IViewController"/>.</param>
        /// <returns>The required <see cref="IViewController"/>.</returns>
        /// <exception cref="Exception"></exception>
        private IViewController GetController(IView view)
        {
            string name;

            if (view is IApplicationView)
            {
                name = Controllers.ApplicationViewController;
            }
            else if (view is IDialogView)
            {
                name = Controllers.GetViewControllerName(view.ID);
            }
            else if (view is IDockableView)
            {
                name = view is IDocumentView ? Controllers.GetDocumentControllerName(view.ID) : Controllers.GetViewControllerName(view.ID);
            }
            else
            {
                throw new Exception(string.Format(Resources.UnexpectedViewType, view.GetType().Name));
            }

            return controllers[name];
        }

        /// <summary>
        /// Gets the <see cref="IView"/> that owns the message box.
        /// </summary>
        /// <returns></returns>
        private IView GetMessageBoxOwner()
        {
            // TODO - Will need to determine the view based on te active view and or information supplied by the calling controller
            // May need to determine the acitve view
            // May need to supply the name of the calling controller or its poarent
            // May need to take into accpunt if the view is docked or floating (centre on workspace or centre on view)

            return views[Views.Application];
        }

        /// <summary>
        /// Initialises the services.
        /// </summary>
        private void InitialiseServices()
        {
            // Do Nothing
        }
    }
}
