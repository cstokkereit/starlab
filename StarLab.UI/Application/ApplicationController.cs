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

    public class ApplicationController : Controller, IApplicationController, ISubscriber<WorkspaceClosedEvent>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ApplicationController));

        private readonly IDictionary<string, IViewController> controllers = new Dictionary<string, IViewController>();

        private readonly IDictionary<string, IView> views = new Dictionary<string, IView>();

        private readonly CommandFactory commandFactory = new CommandFactory();

        private readonly IConfigurationService configuration;

        private readonly IViewFactory viewFactory;

        public ApplicationController(IConfigurationService configuration, IViewFactory viewFactory, IUseCaseFactory factory, IEventAggregator events)
            : base(factory, events)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        public override string Name => Constants.APPLICATION + Constants.CONTROLLER;

        public void Exit()
        {
            if (controllers[Constants.WORKSPACE_VIEW_CONTROLLER] is IWorkspaceController controller) controller.Exit();
        }

        public ICommand CreateCommand(ICommandManager commands, IController controller, string action, string target)
        {
            return commandFactory.CreateCommand(commands, controller, action, target);
        }

        public ICommand CreateCommand(ICommandManager commands, IController controller, string action)
        {
            return commandFactory.CreateCommand(commands, controller, action);
        }

        public ICommand CreateCommand(ICommandManager commands, string view)
        {
            return commandFactory.CreateCommand(commands, this, Actions.SHOW, view);
        }

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
                controllers.Add(controller.Name, controller);
                controller.Initialise(this);
                views.Add(view.ID, view);
            }

            return view;
        }

        public IView GetView(string id)
        {
            return views[id];
        }

        public IWorkspaceController GetWorkspaceController()
        {
            return (IWorkspaceController)controllers[Constants.WORKSPACE_VIEW_CONTROLLER];
        }

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

        public void RegisterCommandInvokers(ICommandManager commands)
        {
            commands.RegisterCommandInvoker(new ToolStripMenuItemCommandInvoker());
            commands.RegisterCommandInvoker(new ToolStripButtonCommandInvoker());
            commands.RegisterCommandInvoker(new ButtonCommandInvoker());
        }

        public void Run()
        {
            log.Info(Resources.InitialisationComplete);

            Events.Subsribe(this);

            InitialiseServices();
            CreateViews();

            var view = views[Views.WORKSPACE];

            if (views[Views.WORKSPACE] is Form form) System.Windows.Forms.Application.Run(form);
        }

        public void Show(IView view)
        {
            var controller = GetController(view);
            controller.Initialise(this);

            controllers[Constants.WORKSPACE_VIEW_CONTROLLER].Show(view); // Need to pick this based on the view in question - somne sort of map?
        }

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

        private void CreateView(string name, string text)
        {
            var view = viewFactory.CreateView(name, text);

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

        private void CreateViews()
        {
            CreateView(Views.ABOUT, Resources.AboutStarLab);
            CreateView(Views.ADD_DOCUMENT, Resources.AddDocument);
            CreateView(Views.OPTIONS, Resources.Options);
            CreateView(Views.WORKSPACE_EXPLORER, Resources.WorkspaceExplorer);

            // NOTE - This must be the last view to be created.
            CreateView(Views.WORKSPACE, Resources.StarLab);
        }

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

        private void InitialiseServices()
        {
            configuration.Initialise();
        }

        private class CommandFactory : Factory
        {
            public ICommand CreateCommand(ICommandManager commands, IController controller, string action, string target)
            {
                return new ActionCommand(commands, controller, action, [target]);

                //return (ICommand)CreateInstance($"{controller.GetType().Namespace}.{action}Command, StarLab.UI", new object[] { commands, controller, target });
            }

            public ICommand CreateCommand(ICommandManager commands, IController controller, string action)
            {
                return new ActionCommand(commands, controller, action);

                //return (ICommand)CreateInstance($"{controller.GetType().Namespace}.{action}Command, StarLab.UI", new object[] { commands, controller });
            }
        }
    }
}
