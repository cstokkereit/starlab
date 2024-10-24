using Castle.Core.Logging;
using StarLab.Application.Workspace;
using StarLab.Commands;
using StarLab.Shared.Properties;

namespace StarLab.Application
{
    // https://github.com/ScottPlot/ScottPlot/blob/main/src/ScottPlot5/ScottPlot5%20Demos/ScottPlot5%20WinForms%20Demo/Demos/DraggableAxisLines.cs

    // https://www.youtube.com/watch?v=280HyyLF-wU

    public class ApplicationController : Controller, IApplicationController, ISubscriber<WorkspaceClosedEvent>
    {
        private readonly IDictionary<string, IViewController> controllers = new Dictionary<string, IViewController>();

        private readonly IViewMap views;

        private readonly ILogger logger;

        public ApplicationController(IViewMap views, IUseCaseFactory factory, IEventAggregator events, ILogger logger)
            : base(factory, events)
        {
            ArgumentNullException.ThrowIfNull(nameof(factory));
            ArgumentNullException.ThrowIfNull(nameof(logger));
            ArgumentNullException.ThrowIfNull(nameof(views));

            this.logger = logger;
            this.views = views;
        }

        public override string Name => Constants.APPLICATION + Constants.CONTROLLER;

        public void Exit()
        {
            if (controllers[Constants.WORKSPACE_VIEW_CONTROLLER] is IWorkspaceController controller)
            {
                var result = controller.ShowMessage(Resources.StarLab, Resources.ApplicationClosing, MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);

                switch (result)
                {
                    case DialogResult.No:
                        controller.Exit();
                        break;

                    case DialogResult.Yes:
                        controller.Exit();
                        break;
                }

                // TODO 
                // Change to a custom dialog that will centre on the application
                // Identify dirty items documentController.Dirty ?
                // Perform any cleanup here prior to closing the workspace
                // Save and/or close all open forms
                // Teardown parent child relationships
                // Implement the save functionality
            }
        }

        public ICommand GetCommand(ICommandManager commands, IController controller, string action, string target)
        {
            ICommand? command = null;

            var type = GetCommandType(controller, action);

            if (type != null)
            {
                command = Activator.CreateInstance(type, new object[] { commands, controller, target }) as ICommand;
            }

            if (command == null) throw new NotImplementedException(); // TODO - Custom Exception

            return command;
        }

        public ICommand GetCommand(ICommandManager commands, IController controller, string action)
        {
            ICommand? command = null;

            var type = GetCommandType(controller, action);

            if (type != null)
            {
                command = Activator.CreateInstance(type, new object[] { commands, controller }) as ICommand;
            }

            if (command == null) throw new NotImplementedException(); // TODO - Custom Exception

            return command;
        }

        public ICommand GetCommand(ICommandManager commands, IViewController controller, string view)
        {
            return new ShowViewCommand(commands, controller, views[view]);
        }

        public IWorkspaceController GetWorkspaceController()
        {
            return (IWorkspaceController)controllers[Constants.WORKSPACE_VIEW_CONTROLLER];
        }

        public void OnEvent(WorkspaceClosedEvent args)
        {
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
            logger.Info(Resources.InitialisationComplete);

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

        private Type? GetCommandType(IController controller, string action)
        {
            return Type.GetType(controller.GetType().Namespace + '.' + action + Constants.COMMAND);
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
    }
}
