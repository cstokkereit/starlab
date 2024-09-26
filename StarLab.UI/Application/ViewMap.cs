using StarLab.Application.Workspace;
using StarLab.Presentation.Model;

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

        #region IDockableViewFactory Members

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

        #endregion

        #region IViewMap Members

        public IView this[string id] => views[id];

        public int Count => views.Count;

        public bool Contains(string id)
        {
            return views.ContainsKey(id);
        }

        public void Initialise()
        {
            CreateView(Views.ABOUT);
            CreateView(Views.OPTIONS);

            CreateToolView(Views.WORKSPACE_EXPLORER);

            // NOTE - This must be the last view to be created.
            CreateView(Views.WORKSPACE);
        }

        public void Remove(string id)
        {
            views.Remove(id);
        }

        #endregion

        private void CreateToolView(string name)
        {
            var view = factory.CreateToolView(name);
            ViewCreated?.Invoke(this, view);
            views.Add(view.ID, view);
        }

        private void CreateView(string name)
        {
            var view = factory.CreateView(name);
            ViewCreated?.Invoke(this, view);
            views.Add(view.ID, view);
        }
    }
}
