using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;

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

        public IDialogView CreateDialogView(string id, string text)
        {
            return new DialogView(id, text, CreateContent(id), presenterFactory);
        }

        public IDockableView CreateDocumentView(IDocument document)
        {
            return (IDockableView)CreateInstance(document.View, new object[] { document, this, presenterFactory });
        }

        public IDockableView CreateToolView(string id, string text)
        {
            return new ToolView(id, text, CreateContent(id), presenterFactory);
        }

        public IWorkspaceView CreateWorkspaceView()
        {
            return new WorkspaceView(Views.WORKSPACE, Resources.StarLab, presenterFactory);
        }

        private IControlView CreateContent(string id)
        {
            return (IControlView)CreateInstance(views[id], new object[] { presenterFactory });
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
    }
}
