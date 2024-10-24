using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;

namespace StarLab.Application
{
    internal class ViewMap : IDockableViewFactory, IViewMap
    {
        private readonly IDictionary<string, IView> views = new Dictionary<string, IView>();

        private readonly IViewFactory factory;

        public ViewMap(IViewFactory factory)
        {
            this.factory = factory;
        }

        public event EventHandler<IView>? ViewCreated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public IDockableView CreateView(IDocument document)
        {
            IDockableView view;

            if (views.ContainsKey(document.ID))
            {
                view = (IDockableView)views[document.ID];
            }
            else
            {
                view = factory.CreateDocumentView(document);
                ViewCreated?.Invoke(this, view);
                views.Add(view.ID, view);
            }

            return view;
        }

        public IDockableView GetView(string id)
        {
            return (IDockableView)views[id];
        }

        public IView this[string id] => views[id];

        public int Count => views.Count;

        public bool Contains(string id)
        {
            return views.ContainsKey(id);
        }

        public void Initialise()
        {
            CreateFormView(Views.ABOUT, Resources.AboutStarLab);
            CreateFormView(Views.OPTIONS, Resources.Options);

            CreateToolView(Views.WORKSPACE_EXPLORER, Resources.WorkspaceExplorer);

            // NOTE - This must be the last view to be created.
            CreateFormView(Views.WORKSPACE, Resources.StarLab);
        }

        public void Remove(string id)
        {
            views.Remove(id);
        }

        private void CreateFormView(string id, string name)
        {
            var view = factory.CreateFormView(id, name);
            ViewCreated?.Invoke(this, view);
            views.Add(view.ID, view);
        }

        private void CreateToolView(string id, string name)
        {
            var view = factory.CreateToolView(id, name);
            ViewCreated?.Invoke(this, view);
            views.Add(view.ID, view);
        }
    }
}
