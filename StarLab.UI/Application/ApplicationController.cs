using Castle.Core.Logging;
using Castle.MicroKernel.SubSystems.Conversion;
using StarLab.Application.Workspace;
using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Shared.Properties;
using System.Windows.Forms;

namespace StarLab.Application
{
    // https://github.com/ScottPlot/ScottPlot/blob/main/src/ScottPlot5/ScottPlot5%20Demos/ScottPlot5%20WinForms%20Demo/Demos/DraggableAxisLines.cs

    // https://www.youtube.com/watch?v=280HyyLF-wU

    public class ApplicationController : Controller, IApplicationController
    {
        private const string COMMAND_NAME = "{0}.{1}({2})";

        private readonly IDictionary<string, IViewController> controllers = new Dictionary<string, IViewController>();

        private readonly ICommandManager commands;

        private readonly IViewMap views;

        private readonly ILogger logger;

        public ApplicationController(IViewMap views, IUseCaseFactory factory, ICommandManager commands, ILogger logger)
            : base(factory)
        {
            this.commands = commands;
            this.logger = logger;
            this.views = views;
        }

        #region IApplicationController Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public ICommand CreateAggregateCommand(IEnumerable<ICommand> commands, string view)
        {
            var name = "DocumentName.ChartSettings"; // use view

            if (!this.commands.ContainsCommand(name))
            {
                this.commands.AddCommand(name, new AggregateCommand(this.commands, commands));
            }

            return this.commands.GetCommand(name);
        }

        //public ICommand GetCommand(ControllerAction<IChartSettingsController> action)
        //{
        //    if (!commands.ContainsCommand(action.Name))
        //    {
        //        commands.AddCommand(action.Name, new ChartSettingsCommand(commands, action));
        //    }

        //    return commands.GetCommand(action.Name);
        //}

        //public ICommand GetCommand(ControllerAction<ISplitViewController> action)
        //{
        //    if (!commands.ContainsCommand(action.Name))
        //    {
        //        commands.AddCommand(action.Name, new SplitViewCommand(commands, action));
        //    }

        //    return commands.GetCommand(action.Name);
        //}

        //public ICommand GetCommand(ControllerAction<IWorkspaceController> action)
        //{
        //    if (!commands.ContainsCommand(action.Name))
        //    {
        //        commands.AddCommand(action.Name, new WorkspaceCommand(commands, action));
        //    }

        //    return commands.GetCommand(action.Name);
        //}

        public ICommand GetCommand(IController controller, string verb, string target)
        {
            var name = string.Format(COMMAND_NAME, controller.Name, verb, target);

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, CreateCommand(controller, verb, target));
            }

            return commands.GetCommand(name);
        }

        public ICommand GetCommand(IController controller, string verb)
        {
            return GetCommand(controller, verb, string.Empty);
        }

        public ICommand GetShowCommand(IViewController controller, string view)
        {
            var name = string.Format(COMMAND_NAME, controller.Name, Verbs.SHOW, view);

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, new ShowCommand(commands, controller, views[view]));
            }

            return commands.GetCommand(name);
        }

        public IWorkspaceController GetWorkspaceController()
        {
            return (IWorkspaceController)controllers[Constants.WORKSPACE_VIEW_CONTROLLER];
        }

        private ICommand CreateCommand(IController controller, string verb, string target)
        {
            ICommand? command = null;

            var type = GetCommandType(controller.GetType().FullName);

            if (type != null)
            {
                command = Activator.CreateInstance(type, new object[] { commands, controller, verb, target }) as ICommand;
            }

            return command;
        }

        private Type GetCommandType(string? controller)
        {
            ArgumentNullException.ThrowIfNull(controller);

            Type? type = null;
            
            switch (controller)
            {
                case "StarLab.Application.SplitViewPresenter":
                    type = Type.GetType("StarLab.Application.SplitViewCommand");
                    break;

                default:
                    type = Type.GetType(controller.Replace("ViewPresenter", Constants.COMMAND));
                    break;
            }

            if (type == null) throw new NotImplementedException();

            return type;
        }

        #endregion

        public void Run()
        {
            commands.RegisterCommandInvoker(new ToolStripMenuItemCommandInvoker());
            commands.RegisterCommandInvoker(new ToolStripButtonCommandInvoker());
            commands.RegisterCommandInvoker(new ButtonCommandInvoker());

            views.ViewCreated += OnViewCreated;

            views.Initialise();

            logger.Info(Resources.MessageStartingStarLab);

            if (views[Views.WORKSPACE] is View view)
            {
                System.Windows.Forms.Application.Run(view);
            }
        }

        protected override string GetName()
        {
            return Constants.APPLICATION + Constants.CONTROLLER;
        }

        private void OnViewCreated(object? sender, IView? view)
        {
            ArgumentNullException.ThrowIfNull(view);

            if (view is IWorkspaceView workspaceView)
            {
                workspaceView.Initialise(this, (IDockableViewFactory)views);
            }
            else
            {
                view.Initialise(this);
            }

            controllers.Add(view.Controller.Name, view.Controller);
        }
    }
}
