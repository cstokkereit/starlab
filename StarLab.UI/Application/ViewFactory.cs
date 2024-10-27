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

        public IControlView CreateControlView(string typeName)
        {
            return (IControlView)CreateInstance(typeName, new object[] { presenterFactory });
        }

        public IDockableView CreateDocumentView(IDocument document)
        {
            return (IDockableView)CreateInstance(document.View, new object[] { document, this, presenterFactory });
        }

        public IFormView CreateFormView(string id, string name)
        {
            IFormView? view = null;

            if (id == Views.WORKSPACE)
            {
                view = new WorkspaceView(id, name, presenterFactory);
            }
            else
            {
                view = new View(id, name, CreateContent(id), presenterFactory);
            }

            return view;
        }

        public IDockableView CreateToolView(string id, string name)
        {
            return new ToolView(id, name, CreateContent(id), presenterFactory);
        }

        private IControlView CreateContent(string id)
        {


            return (IControlView)CreateInstance(views[id], new object[] { presenterFactory });
        }

        private void Initialise()
        {
            views.Add(Views.ABOUT, "StarLab.Application.Help.AboutView, StarLab.UI");
            views.Add(Views.CHART, "StarLab.Application.Workspace.Documents.Charts.ChartView, StarLab.UI");
            views.Add(Views.CHART_SETTINGS, "StarLab.Application.Workspace.Documents.Charts.ChartSettingsView, StarLab.UI");
            views.Add(Views.OPTIONS, "StarLab.Application.Options.OptionsView, StarLab.UI");
            views.Add(Views.WORKSPACE, "StarLab.Application.Workspace.WorkspaceView, StarLab.UI");
            views.Add(Views.WORKSPACE_EXPLORER, "StarLab.Application.Workspace.WorkspaceExplorer.WorkspaceExplorerView, StarLab.UI");
        }
    }
}
