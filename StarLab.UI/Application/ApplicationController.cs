using log4net;
using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;
using StarLab.Shared.Properties;

namespace StarLab.Application
{
    // https://github.com/ScottPlot/ScottPlot/blob/main/src/ScottPlot5/ScottPlot5%20Demos/ScottPlot5%20WinForms%20Demo/Demos/DraggableAxisLines.cs

    // https://www.youtube.com/watch?v=280HyyLF-wU

    /// <summary>
    /// A controller that creates, initialises and manages the views that comprise the user interface of the application.
    /// </summary>
    public class ApplicationController : Controller, IApplicationController, ISubscriber<WorkspaceClosedEvent>
    {
        private readonly IDictionary<string, IViewController> controllers = new Dictionary<string, IViewController>(); // A dictionary containing the view controllers indexed by name.

        private static readonly ILog log = LogManager.GetLogger(typeof(ApplicationController)); // The logger that will be used for writing log messages.

        private readonly IDictionary<string, IView> views = new Dictionary<string, IView>(); // A dictionary containing the views indexed by ID. 

        private readonly IConfigurationService configuration; // A service that provides the configuration information.

        private readonly IPresentationFactory factory; // A factory for creating views.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfigurationService"> that provides the configuration information.</param>
        /// <param name="factory">An <see cref="IPresentationFactory"/> that will be used to create the views.</param>
        /// <param name="interactorFactory">An <see cref="IUseCaseFactory"> that will be used to create the use case interactors.</param>
        /// <param name="events">An <see cref="IEventAggregator"> that can be used for subscribing to and publishing events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ApplicationController(IConfigurationService configuration, IPresentationFactory factory, IUseCaseFactory interactorFactory, IEventAggregator events)
            : base(interactorFactory, events)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Name => Constants.APPLICATION + Constants.CONTROLLER;

        /// <summary>
        /// Exits the application.
        /// </summary>
        public void Exit()
        {
            if (controllers[Constants.WORKSPACE_VIEW_CONTROLLER] is IWorkspaceController controller) controller.Exit();
        }

        /// <summary>
        /// Creates the <see cref="ICommand"> specified by the controller, action and target provided.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
        /// <param name="action">The action to be performed by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
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
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
        /// <param name="action">The action to be performed by the <see cref="ICommand"/> when it's <see cref="Execute"/> method is called.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to invoke the specified action.</returns>
        public ICommand CreateCommand(ICommandManager commands, IController controller, string action)
        {
            return new ActionCommand(commands, controller, action);
        }

        /// <summary>
        /// Creates an <see cref="ICommand"> that will show the specified view.
        /// </summary>
        /// <param name="commands">An instance of <see cref="ICommandManager"/> that is required for the creation of the command.</param>
        /// <param name="view">The name of the <see cref="IView"/> to be shown.</param>
        /// <returns>An instance of <see cref="ICommand"> that can be used to show the specified view.</returns>
        public ICommand CreateCommand(ICommandManager commands, string view)
        {
            return CreateCommand(commands, this, Actions.SHOW, view);
        }

        /// <summary>
        /// Gets the <see cref="IView"/> specified by the <see cref="IDocument"/> provided. If the view does not already exist it will be created.
        /// </summary>
        /// <param name="document">An instance of <see cref="IDocument"/> that specifies which instance of <see cref="IView"/> is required.</param>
        /// <returns>The required instance of <see cref="IView">.</returns>
        public IView GetView(IDocument document)
        {
            IView view;

            if (views.ContainsKey(document.ID))
            {
                view = views[document.ID];
            }
            else
            {
                view = factory.CreateView(document);

                var controller = ((DocumentView)view).Controller;
                controllers.Add(controller.Name, controller);
                controller.Initialise(this);

                views.Add(view.ID, view);
            }

            return view;
        }

        /// <summary>
        /// Gets the <see cref="IView"/> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the required instance of <see cref="IView"/>.</param>
        /// <returns>The required instance of <see cref="IView">.</returns>
        public IView GetView(string id)
        {
            return views[id];
        }

        /// <summary>
        /// TODO - Is there a better way than doing this?
        /// </summary>
        /// <returns></returns>
        public IWorkspaceController GetWorkspaceController()
        {
            return (IWorkspaceController)controllers[Constants.WORKSPACE_VIEW_CONTROLLER];
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="args"></param>
        public void OnEvent(WorkspaceClosedEvent args)
        {
            // TODO
            // Change to a custom dialog that will centre on the application
            // Perform any cleanup here prior to closing the workspace
            // Teardown parent child relationships

            //foreach (var document in args.Workspace.Documents)
            //{
            //    controllers.Remove($"Document ({document.ID}) Controller");
            //    views.Remove(document.ID);
            //}
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
            log.Info(Resources.InitialisationComplete);

            Events.Subsribe(this);

            InitialiseServices();
            CreateViews();

            var view = views[Views.WORKSPACE];

            if (views[Views.WORKSPACE] is Form form) System.Windows.Forms.Application.Run(form);
        }

        /// <summary>
        /// Shows the <see cref="IView"> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"> to be shown.</param>
        public void Show(IView view)
        {
            var controller = GetController(view);
            controller.Initialise(this);

            controllers[Constants.WORKSPACE_VIEW_CONTROLLER].Show(view); // Need to pick this based on the view in question - somne sort of map?
        }

        /// <summary>
        /// Shows the <see cref="IView"> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the view to be shown.</param>
        public void Show(string id)
        {
            if (!views.ContainsKey(id)) throw new ArgumentOutOfRangeException(nameof(id)); // TODO

            var view = views[id];

            var controller = GetController(view);
            controller.Initialise(this);

            //if (view is IDialog dialog)
            //    dialog.Show(new AddDocumentDialogConfiguration(workspace, path, DocumentType.Chart)); This is how AddDcoumentView gets shown from WorkspaceExplorerViewPresenter

            controllers[Constants.WORKSPACE_VIEW_CONTROLLER].Show(view); // Need to pick this based on the view in question - some sort of map?
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void CreateView(string name, string text)
        {
            var view = factory.CreateView(name, text);

            IViewController controller;

            switch (configuration.GetViewConfiguration(name).Type)
            {
                case ViewTypes.Application:
                    controller = ((WorkspaceView)view).Controller;
                    break;

                case ViewTypes.Dialog:
                    controller = ((DialogView)view).Controller;
                    break;

                case ViewTypes.Document:
                    throw new NotImplementedException();

                case ViewTypes.Tool:
                    controller = ((ToolView)view).Controller;
                    break;

                default:
                    throw new NotImplementedException(); // TODO
            }

            controllers.Add(controller.Name, controller);
            controller.Initialise(this);
            views.Add(view.ID, view);
        }

        /// <summary>
        /// Creates all of the fixed views. This excludes any views that implement <see cref="IDocumentView"/> as these are created in response to a workspace being loaded or modified.
        /// </summary>
        private void CreateViews()
        {
            CreateView(Views.ABOUT, Resources.AboutStarLab);
            CreateView(Views.ADD_DOCUMENT, Resources.AddDocument);
            CreateView(Views.OPTIONS, Resources.Options);
            CreateView(Views.WORKSPACE_EXPLORER, Resources.WorkspaceExplorer);

            // NOTE - This must be the last view to be created.
            CreateView(Views.WORKSPACE, Resources.StarLab);
        }

        /// <summary>
        /// Gets the <see cref="IViewController"/> that controls the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that is controlled by the required <see cref="IViewController"/>.</param>
        /// <returns>The specified <see cref="IViewController"/>.</returns>
        /// <exception cref="Exception"></exception>
        private IViewController GetController(IView view)
        {
            string id;

            if (view is IWorkspaceView)
            {
                id = Views.WORKSPACE + Constants.CONTROLLER;
            } 
            else if (view is IDialogView)
            {
                id = view.ID + Constants.CONTROLLER;
            }
            else if (view is IDocumentView)
            {
                id = $"Document({view.ID}) {Constants.CONTROLLER}";
            }
            else if (view is IDockableView)
            {
                id = view.ID + Constants.CONTROLLER;
            }
            else
            {
                throw new Exception(); // TODO
            }

            return controllers[id];
        }

        /// <summary>
        /// Initialises the services.
        /// </summary>
        private void InitialiseServices()
        {
            configuration.Initialise();
        }
    }
}
