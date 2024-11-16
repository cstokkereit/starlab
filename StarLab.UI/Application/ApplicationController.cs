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
            ArgumentNullException.ThrowIfNull(views, nameof(views));

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

        public ICommand CreateCommand(ICommandManager commands, string view)
        {
            return commandFactory.CreateCommand(commands, this, Actions.SHOW, view);
        }

        public IDialogView GetDialog(string name)
        {
            var view = views[name] as IDialogView;

            return view ?? throw new ArgumentOutOfRangeException(nameof(name)); // TODO
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

            views.ViewCreated += OnViewCreated;

            Events.Subsribe(this);

            views.Initialise();

            if (views[Views.WORKSPACE] is Form form) System.Windows.Forms.Application.Run(form);
        }

        public void Show(IDialogView view)
        {
            if (controllers.ContainsKey(Constants.WORKSPACE_VIEW_CONTROLLER))
            {
                controllers[Constants.WORKSPACE_VIEW_CONTROLLER].Show(view);
            }
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
            ArgumentNullException.ThrowIfNull(view, nameof(view));

            if (view is IWorkspaceView workspaceView)
            {
                workspaceView.Initialise(this, (IDockableViewFactory)views);
            }
            else if (view is IDockableView dockableView)
            {
                dockableView.Initialise(this);
            }
            else if (view is IDialogView dialogView)
            {
                dialogView.Initialise(this);
            }

            controllers.Add(view.Controller.Name, view.Controller);
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
