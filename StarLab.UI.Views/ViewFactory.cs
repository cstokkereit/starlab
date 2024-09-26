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

        #region IViewFactory Members

        public IDockableView CreateDocumentView(IDocument document)
        {
            IDockableView? view = null;

            var typeName = views[Views.DOCUMENT];

            var type = Type.GetType(typeName);

            if (type != null)
            {
                var content = CreateContent(document.Content);

                view = Activator.CreateInstance(type, new object[] { document, content, presenterFactory }) as IDockableView;
            }

            if (view == null) throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));

            return view;
        }

        public IDockableView CreateToolView(string name)
        {
            IDockableView? view = null;

            var typeName = views[Views.TOOL];

            var type = Type.GetType(typeName);

            if (type != null)
            {
                var content = CreateContent(name);

                view = Activator.CreateInstance(type, new object[] { name, content, presenterFactory }) as IDockableView;
            }

            if (view == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return view;
        }

        public IFormView CreateView(string name)
        {
            IFormView? view = null;

            var typeName = views[name];

            var type = Type.GetType(typeName);

            if (type != null)
            {
                view = Activator.CreateInstance(type, new object[] { presenterFactory }) as IFormView;
            }

            if (view == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return view;
        }

        #endregion

        private IControlView CreateContent(string name)
        {
            IControlView? view = null;

            var typeName = views[name];

            var type = Type.GetType(typeName);

            if (type != null)
            {
                view = Activator.CreateInstance(type, new object[] { presenterFactory }) as IControlView;
            }

            if (view == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return view;
        }

        private IControlView CreateContent(IContent content)
        {
            IControlView? view = null;

            view = CreateContent(content.View);

            if (view is ISplitView splitView)
            {
                foreach (var childContent in content.Contents)
                {
                    splitView.AddChild(CreateContent(childContent), childContent.Panel);
                }
            }

            return view;
        }

        private void Initialise()
        {
            views.Add(Views.ABOUT, "StarLab.Application.Help.AboutView");
            views.Add(Views.COLOUR_MAGNITUDE_CHART, "StarLab.Application.Workspace.Documents.Charts.ColourMagnitudeChartView");
            views.Add(Views.CHART_SETTINGS, "StarLab.Application.Workspace.Documents.Charts.ChartSettingsView");
            views.Add(Views.DOCUMENT, "StarLab.Application.Workspace.Documents.DocumentView");
            views.Add(Views.OPTIONS, "StarLab.Application.Options.OptionsView");
            views.Add(Views.SPLIT_CONTAINER, "StarLab.Application.SplitView");
            views.Add(Views.TOOL, "StarLab.Application.Workspace.ToolView");
            views.Add(Views.WORKSPACE, "StarLab.Application.Workspace.WorkspaceView");
            views.Add(Views.WORKSPACE_EXPLORER, "StarLab.Application.Workspace.WorkspaceExplorer.WorkspaceExplorerView");
        }
    }
}
