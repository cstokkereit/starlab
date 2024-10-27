using log4net;
using StarLab.Application.Workspace;
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

        private readonly CommandFactory commandFactory;

        private readonly IViewMap views;

        public ApplicationController(IViewMap views, IUseCaseFactory factory, IEventAggregator events)
            : base(factory, events)
        {
            ArgumentNullException.ThrowIfNull(nameof(views));

            commandFactory = new CommandFactory();

            this.views = views;
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

        public ICommand CreateCommand(ICommandManager commands, IViewController controller, string view)
        {
            return new ShowViewCommand(commands, controller, views[view]);
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

            foreach (var document in args.Workspace.Documents)
            {
                controllers.Remove(string.Format(Constants.DOCUMENT_CONTROLLER, document.ID));
                views.Remove(document.ID);
            }
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

            views.ViewCreated += OnViewCreated;

            Events.Subsribe(this);

            views.Initialise();

            if (views[Views.WORKSPACE] is Form form)
                System.Windows.Forms.Application.Run(form);
        }

        public void Show(string id)
        {
            if (views.Contains(id) && controllers.ContainsKey(Constants.WORKSPACE_VIEW_CONTROLLER))
            {
                controllers[Constants.WORKSPACE_VIEW_CONTROLLER].Show(views[id]);
            }
        }

        private void OnViewCreated(object? sender, IView? view)
        {
            ArgumentNullException.ThrowIfNull(view);

            if (view is IWorkspaceView workspaceView)
            {
                workspaceView.Initialise(this, (IDockableViewFactory)views);
            }
            else if (view is IDockableView dockableView)
            {
                dockableView.Initialise(this);
            }

            controllers.Add(view.Controller.Name, view.Controller);
        }

        private class CommandFactory : Factory
        {
            private const string TYPE_NAME = "{0}.{1}Command, StarLab.UI";

            public ICommand CreateCommand(ICommandManager commands, IController controller, string action, string target)
            {
                return (ICommand)CreateInstance(string.Format(TYPE_NAME, controller.GetType().Namespace, action), new object[] { commands, controller, target });
            }

            public ICommand CreateCommand(ICommandManager commands, IController controller, string action)
            {
                return (ICommand)CreateInstance(string.Format(TYPE_NAME, controller.GetType().Namespace, action), new object[] { commands, controller });
            }
        }
    }
}
