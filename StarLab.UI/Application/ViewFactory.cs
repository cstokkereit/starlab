using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public class ViewFactory : Factory, IViewFactory
    {
        private readonly Dictionary<string, string> views = new Dictionary<string, string>();

        private readonly IPresenterFactory presenterFactory;

        public ViewFactory(IPresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory ?? throw new ArgumentNullException(nameof(presenterFactory));

            Initialise();
        }

        public IChildView CreateControlView(string typeName)
        {
            return (IChildView)CreateInstance(typeName, new object[] { presenterFactory });
        }

        public IViewBundle CreateDialogView(string id, string text)
        {
            var view = new DialogView(id, text, CreateContent(id), presenterFactory);
            return new ViewBundle(view, view.Controller);
        }

        public IViewBundle CreateDocumentView(IDocument document)
        {
            var view = (DocumentView)CreateInstance(document.View, new object[] { document, this, presenterFactory });
            return new ViewBundle(view, view.Controller);
        }

        public IViewBundle CreateToolView(string id, string text)
        {
            var view = new ToolView(id, text, CreateContent(id), presenterFactory);
            return new ViewBundle(view, view.Controller);
        }

        public IViewBundle CreateWorkspaceView()
        {
            var view = new WorkspaceView(presenterFactory);
            return new ViewBundle(view, view.Controller);
        }

        private IChildView CreateContent(string id)
        {
            return (IChildView)CreateInstance(views[id], new object[] { presenterFactory });
        }

        private void Initialise()
        {
            views.Add(Views.ABOUT, "StarLab.Application.Help.AboutView, StarLab.UI");
            views.Add(Views.ADD_DOCUMENT, "StarLab.Application.Workspace.Documents.AddDocumentView, StarLab.UI");
            views.Add(Views.CHART, "StarLab.Application.Workspace.Documents.Charts.ChartView, StarLab.UI");
            views.Add(Views.CHART_SETTINGS, "StarLab.Application.Workspace.Documents.Charts.ChartSettingsView, StarLab.UI");
            views.Add(Views.OPTIONS, "StarLab.Application.Options.OptionsView, StarLab.UI");
            views.Add(Views.WORKSPACE, "StarLab.Application.Workspace.WorkspaceView, StarLab.UI");
            views.Add(Views.WORKSPACE_EXPLORER, "StarLab.Application.Workspace.WorkspaceExplorer.WorkspaceExplorerView, StarLab.UI");
        }

        private struct ViewBundle : IViewBundle
        {
            public ViewBundle(IView view, IViewController controller)
            {
                Controller = controller;
                View = view;
            }

            public IViewController Controller { get; private set; }

            public IView View { get; private set; }
        }
    }
}
