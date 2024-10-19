using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation;
using StarLab.Presentation.Model;
using StarLab.Shared.Properties;

namespace StarLab
{
    public class ViewFactory : IViewFactory
    {
        private readonly Dictionary<string, string> views = new Dictionary<string, string>();

        private readonly IPresenterFactory presenterFactory;

        public ViewFactory(IPresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory;

            Initialise();
        }

        public IControlView CreateControlView(string typeName)
        {
            IControlView? view = null;

            var type = Type.GetType(typeName);

            if (type != null)
                view = Activator.CreateInstance(type, new object[] { presenterFactory }) as IControlView;

            if (view == null)
                throw new Exception(string.Format(Resources.CouldNotBeCreated, typeName));

            return view;
        }

        public IDockableView CreateDocumentView(IDocument document)
        {
            IDockableView? view = null;

            var type = Type.GetType(document.View);

            if (type != null)
                view = Activator.CreateInstance(type, new object[] { document, this, presenterFactory }) as IDockableView;
            
            if (view == null) 
                throw new Exception(string.Format(Resources.CouldNotBeCreated, document.View));

            return view;
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
            IControlView? view = null;

            var typeName = views[id];

            var type = Type.GetType(typeName);

            if (type != null)
                view = Activator.CreateInstance(type, new object[] { presenterFactory }) as IControlView;

            if (view == null)
                throw new Exception(string.Format(Resources.CouldNotBeCreated, typeName));

            return view;
        }

        private void Initialise()
        {
            views.Add(Views.ABOUT, "StarLab.Application.Help.AboutView");
            views.Add(Views.CHART_SETTINGS, "StarLab.Application.Workspace.Documents.Charts.ChartSettingsView");
            views.Add(Views.COLOUR_MAGNITUDE_CHART, "StarLab.Application.Workspace.Documents.Charts.ColourMagnitudeChartView");
            views.Add(Views.OPTIONS, "StarLab.Application.Options.OptionsView");
            views.Add(Views.WORKSPACE, "StarLab.Application.Workspace.WorkspaceView");
            views.Add(Views.WORKSPACE_EXPLORER, "StarLab.Application.Workspace.WorkspaceExplorer.WorkspaceExplorerView");
        }
    }
}
