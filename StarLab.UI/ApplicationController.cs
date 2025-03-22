using log4net;
using StarLab.Application;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using StarLab.UI.Controls;
using StarLab.UI.Workspace;
using StarLab.UI.Workspace.Documents;
using Stratosoft.Commands;

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

        private readonly IConfigurationProvider configuration; // A service that provides the configuration information.

        private readonly IViewFactory factory; // A factory for creating views.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfigurationProvider"> that provides the configuration information.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the views.</param>
        /// <param name="interactorFactory">An <see cref="IUseCaseFactory"> that will be used to create the use case interactors.</param>
        /// <param name="events">An <see cref="IEventAggregator"> that can be used for subscribing to and publishing events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ApplicationController(IConfigurationProvider configuration, IViewFactory factory, IUseCaseFactory interactorFactory, IEventAggregator events)
            : base(interactorFactory, events)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => ControllerNames.APPLICATION_CONTROLLER;

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
            return CreateCommand(commands, this, Actions.SHOW, view);
        }

        /// <summary>
        /// Deletes the <see cref="IView"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IView"/> to delete.</param>
        public void DeleteView(string id)
        {
            if (views.ContainsKey(id))
            {
                var view = views[id];

                if (controllers[$"Document({view.ID}) Controller"] is IDocumentController controller) // If other types move to IViewController
                {
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
            if (controllers[ControllerNames.APPLICATION_VIEW_CONTROLLER] is IApplicationViewController controller) controller.Exit();
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"/> that controls the content of the specified view.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <returns>The required <see cref="IChildViewController"/>.</returns>
        //public IChildViewController GetContentController(string name)
        //{
        //    IChildViewController? controller = null;

        //    var view = GetView(name);

        //    if (view != null)
        //    {
        //        //if (view is ToolView toolView) controller = toolView.ContentController;
        //    }

        //    if (controller == null) throw new Exception(); // TODO

        //    return controller;
        //}

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
                view = factory.CreateView(document);

                var controller = ((DocumentView)view).Controller;
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
        /// TODO
        /// </summary>
        /// <param name="args"></param>
        public void OnEvent(WorkspaceClosedEventArgs args)
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
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            var controller = GetController(view);
            controller.Initialise(this);

            controllers[ControllerNames.APPLICATION_VIEW_CONTROLLER].Show(view);
        }

        /// <summary>
        /// Shows the <see cref="IView"/> with the specified ID. A view with the specified ID must already exist or an exception will be thrown.
        /// </summary>
        /// <param name="id">The ID of the view to be shown.</param>
        public void Show(string id)
        {
            if (!views.ContainsKey(id)) throw new ArgumentOutOfRangeException(nameof(id)); // TODO

            var view = views[id];

            var controller = GetController(view);
            controller.Initialise(this);

            controllers[ControllerNames.APPLICATION_VIEW_CONTROLLER].Show(view); // Need to pick this based on the view in question - some sort of map?
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
                    controller = ((ApplicationView)view).Controller;
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
        /// <param name="view">The <see cref="IView"/> that is controlled by the <see cref="IViewController"/>.</param>
        /// <returns>The required <see cref="IViewController"/>.</returns>
        /// <exception cref="Exception"></exception>
        private IViewController GetController(IView view)
        {
            string name;

            if (view is IApplicationView)
            {
                name = ControllerNames.APPLICATION_VIEW_CONTROLLER;
            }
            else if (view is IDialogView)
            {
                name = ControllerNames.GetViewControllerName(view.ID);
            }
            else if (view is IDockableView)
            {
                name = view is IDocumentView ? ControllerNames.GetDocumentControllerName(view.ID) : ControllerNames.GetViewControllerName(view.ID);
            }
            else
            {
                throw new Exception(); // TODO
            }

            return controllers[name];
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
