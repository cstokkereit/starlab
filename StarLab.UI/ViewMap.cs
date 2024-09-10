using StarLab.Presentation;
using StarLab.Presentation.Docking;
using StarLab.Presentation.Model;
using StarLab.Presentation.Workspaces;
using StarLab.Shared.Properties;

namespace StarLab.UI
{
    internal class ViewMap : IDockableViewFactory, IViewMap
    {
        private readonly IDictionary<string, IView> views = new Dictionary<string, IView>();

        private readonly IViewFactory factory;

        public ViewMap(IViewFactory factory)
        {
            this.factory = factory;
        }

        #region IDockableViewFactory Members

        public event EventHandler<IView> DocumentCreated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public IDockableView CreateView(IDocument document)
        {
            IDockableView view;

            if (views.ContainsKey(document.FullName))
            {
                view = (IDockableView)views[document.FullName];
            }
            else
            {
                var context = new DocumentViewContext(document);
                view = factory.CreateView(context);
                views.Add(view.Name, view);
            }
            
            DocumentCreated?.Invoke(this, view);

            return view;
        }

        public IDockableView GetView(string name)
        {
            return (IDockableView)views[name];
        }

        #endregion

        #region IViewMap Members

        public IView this[string name] => views[name];

        public int Count => views.Count;

        public bool Contains(string name)
        {
            return views.ContainsKey(name);
        }

        public void Initialise(IApplicationController controller)
        {
            CreateView(Views.ABOUT, controller);
            CreateView(Views.OPTIONS, controller);

            CreateView(Views.WORKSPACE_EXPLORER, Resources.WorkspaceExplorer, Views.WORKSPACE_EXPLORER, Constants.DOCK_RIGHT, controller);

            // NOTE - This must be the last view to be created.
            CreateView(Views.WORKSPACE, controller);
        }

        public void Remove(string name)
        {
            views.Remove(name);
        }

        #endregion

        private void CreateView(string name, IApplicationController controller)
        {
            var view = factory.CreateView(name);

            if (view is IWorkspaceView mainView)
            {
                mainView.Initialise(controller, this);
            }
            else
            {
                view.Initialise(controller);
            }
            
            views.Add(view.Name, view);
        }

        private void CreateView(IViewContext context, IApplicationController controller)
        {
            var view = factory.CreateView(context);
            view.Initialise(controller);
            views.Add(view.Name, view);
        }

        private void CreateView(string name, string text, string view, string defaultLocation, IApplicationController controller) // TODO - Improve this
        {
            CreateView(new ToolViewContext(name, text, defaultLocation, new Content(view)), controller);
        }
    }
}
