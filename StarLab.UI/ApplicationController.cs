using Castle.Core.Logging;
using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Presentation.Charts;
using StarLab.Presentation.Workspaces;
using StarLab.Presentation.Workspaces.WorkspaceExplorer;
using StarLab.Shared.Properties;
using StarLab.UI.Commands;

namespace StarLab.UI
{
    // https://github.com/ScottPlot/ScottPlot/blob/main/src/ScottPlot5/ScottPlot5%20Demos/ScottPlot5%20WinForms%20Demo/Demos/DraggableAxisLines.cs

    // https://www.youtube.com/watch?v=280HyyLF-wU

    public class ApplicationController : IApplicationController
    {
        private readonly ICommandManager commands;

        private readonly IViewMap views;

        private readonly ILogger logger;

        public ApplicationController(IViewMap views, ICommandManager commands, ILogger logger)
        {
            this.commands = commands;
            this.logger = logger;
            this.views = views;
        }

        #region IApplicationController Members

        //public IDocumentViewFactory ViewFactory => views;

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

        public ICommand GetCommand(ControllerAction<IChartSettingsController> action)
        {
            if (!commands.ContainsCommand(action.Name))
            {
                commands.AddCommand(action.Name, new ChartSettingsCommand(commands, action));
            }

            return commands.GetCommand(action.Name);
        }

        public ICommand GetCommand(ControllerAction<ISplitViewController> action)
        {
            if (!commands.ContainsCommand(action.Name))
            {
                commands.AddCommand(action.Name, new SplitViewCommand(commands, action));
            }

            return commands.GetCommand(action.Name);
        }

        public ICommand GetCommand(ControllerAction<IWorkspaceController> action)
        {
            if (!commands.ContainsCommand(action.Name))
            {
                commands.AddCommand(action.Name, new WorkspaceCommand(commands, action));
            }

            return commands.GetCommand(action.Name);
        }

        public ICommand GetCommand(ControllerAction<IWorkspaceExplorerController> action)
        {
            if (!commands.ContainsCommand(action.Name))
            {
                commands.AddCommand(action.Name, new WorkspaceExplorerCommand(commands, action));
            }

            return commands.GetCommand(action.Name);
        }

        public ICommand GetCommand(ControllerAction<IViewController> action)
        {
            if (!commands.ContainsCommand(action.Name))
            {
                commands.AddCommand(action.Name, new ShowViewCommand(commands, action.Controller, views[action.Target]));
            }

            return commands.GetCommand(action.Name);
        }

        #endregion

        public void Run()
        {
            commands.RegisterCommandInvoker(new ToolStripMenuItemCommandInvoker());
            commands.RegisterCommandInvoker(new ToolStripButtonCommandInvoker());
            commands.RegisterCommandInvoker(new ButtonCommandInvoker());

            views.DocumentCreated += OnDocumentCreated;

            views.Initialise(this);

            logger.Info(Resources.MessageStartingStarLab);

            if (views[Views.WORKSPACE] is View view)
            {
                System.Windows.Forms.Application.Run(view);
            }
        }

        private void OnDocumentCreated(object? sender, IView? view)
        {
            view?.Initialise(this);
        }
    }
}
