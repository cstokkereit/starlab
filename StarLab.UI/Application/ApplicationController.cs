using log4net;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;
using StarLab.Shared.Properties;
using System.Reflection.Metadata;

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

        private readonly IViewFactory viewFactory;

        public ApplicationController(IUseCaseFactory factory, IViewFactory viewFactory, IEventAggregator events)
            : base(factory, events)
        {
            ArgumentNullException.ThrowIfNull(viewFactory, nameof(viewFactory));

            this.viewFactory = viewFactory;
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

        public IDockableView GetView(IDocument document)
        {
            IView view;

            if (views.ContainsKey(document.ID))
            {
                view = (IDockableView)views[document.ID];
            }
            else
            {
                var bundle = viewFactory.CreateDocumentView(document);

                var controller = bundle.Controller;
                controllers.Add(controller.Name, controller);
                controller.Initialise(this);

                view = bundle.View;
                views.Add(view.ID, view);
            }

            return (IDockableView)view;
        }

        public IDockableView GetView(string id)
        {
            return (IDockableView)views[id];
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

            CreateViews();
            InitialiseViews();

            var view = views[Views.WORKSPACE];

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
            if (views.ContainsKey(id))
            {
                var view = views[id];

                var controller = GetController(view);
                controller.Initialise(this);

                controllers[Constants.WORKSPACE_VIEW_CONTROLLER].Show(views[id]);
            }
        }

        private void CreateDialogView(string id, string name)
        {
            var bundle = viewFactory.CreateDialogView(id, name);

            var controller = bundle.Controller;
            controllers.Add(controller.Name, controller);
            views.Add(bundle.View.ID, bundle.View);
        }

        private void CreateToolView(string id, string name)
        {
            var bundle = viewFactory.CreateToolView(id, name);

            var controller = bundle.Controller;
            controllers.Add(controller.Name, controller);
            views.Add(bundle.View.ID, bundle.View);
        }

        private void CreateWorkspaceView()
        {
            var bundle = viewFactory.CreateWorkspaceView();

            var controller = bundle.Controller;
            controllers.Add(controller.Name, controller);
            views.Add(bundle.View.ID, bundle.View);
        }

        private void CreateViews()
        {
            CreateDialogView(Views.ABOUT, Resources.AboutStarLab);
            CreateDialogView(Views.ADD_DOCUMENT, Resources.AddDocument);
            CreateDialogView(Views.OPTIONS, Resources.Options);

            CreateToolView(Views.WORKSPACE_EXPLORER, Resources.WorkspaceExplorer);

            // NOTE - This must be the last view to be created.
            CreateWorkspaceView();
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
            else if (view is IDockableView)
            {
                id = view.ID + Constants.CONTROLLER;
            }
            else if (view is IDocumentView)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new Exception(); // TODO
            }

            return controllers[id];
        }

        private void InitialiseViews()
        {
            GetController(views[Views.WORKSPACE_EXPLORER]).Initialise(this);

            // NOTE - This must be the last view to be initialised
            GetController(views[Views.WORKSPACE]).Initialise(this);
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
