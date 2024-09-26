using AutoMapper;
using StarLab.Application;
using StarLab.Application.Events;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Model;
using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly Dictionary<string, string> presenters = new Dictionary<string, string>();

        private readonly IUseCaseFactory useCaseFactory;

        private readonly IConfiguration configuration;

        private readonly IEventAggregator events;

        private readonly IMapper mapper;

        public PresenterFactory(IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
        {
            this.useCaseFactory = useCaseFactory;
            this.configuration = configuration;
            this.events = events;
            this.mapper = mapper;

            Initialise();
        }

        public IControlViewPresenter CreatePresenter(IControlView view, string presenterTypeName)
        {
            throw new NotImplementedException();

            //IControlViewPresenter? presenter = null;

            //var type = Type.GetType(presenterTypeName);

            //if (type != null)
            //{
            //    presenter = Activator.CreateInstance(type, new object[] { view, useCaseFactory, configuration, mapper, events }) as IControlViewPresenter;
            //}

            //if (presenter == null)
            //{
            //    throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, presenterTypeName));
            //}

            //return presenter;
        }

        public IControlViewPresenter CreatePresenter(IControlView view)
        {
            IControlViewPresenter? presenter = null;

            var typeName = presenters[view.Name];

            var type = Type.GetType(typeName);

            if (type != null)
            {
                presenter = Activator.CreateInstance(type, new object[] { view, useCaseFactory, configuration, mapper, events }) as IControlViewPresenter;
            }

            if (presenter == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return presenter;
        }

        public IDockableViewPresenter CreatePresenter(IDockableView view)
        {
            return new ToolViewPresenter(view, useCaseFactory, configuration, mapper, events);
        }

        public IDockableViewPresenter CreatePresenter(IDockableView view, IDocument document)
        {
            return new DocumentViewPresenter(view, document, useCaseFactory, configuration, mapper, events);
        }

        public IFormViewPresenter CreatePresenter(IView view)
        {
            IFormViewPresenter? presenter = null;

            var typeName = presenters[view.Name];

            var type = Type.GetType(typeName);

            if (type != null)
            {
                presenter = Activator.CreateInstance(type, new object[] { view, useCaseFactory, configuration, mapper, events }) as IFormViewPresenter;
            }

            if (presenter == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return presenter;
        }

        public ISplitViewPresenter CreatePresenter(ISplitView view)
        {
            return new SplitViewPresenter(view, useCaseFactory, configuration, mapper, events);
        }

        private void Initialise()
        {
            presenters.Add(Views.ABOUT, "StarLab.Application.Help.AboutViewPresenter");
            presenters.Add(Views.CHART_SETTINGS, "StarLab.Application.Workspace.Documents.Charts.ChartSettingsViewPresenter");
            presenters.Add(Views.COLOUR_MAGNITUDE_CHART, "StarLab.Application.Workspace.Documents.Charts.ColourMagnitudeChartViewPresenter");
            presenters.Add(Views.OPTIONS, "StarLab.Application.Options.OptionsViewPresenter");
            presenters.Add(Views.WORKSPACE, "StarLab.Application.Workspace.WorkspaceViewPresenter");
            presenters.Add(Views.WORKSPACE_EXPLORER, "StarLab.Application.Workspace.WorkspaceExplorer.WorkspaceExplorerViewPresenter");
        }
    }
}
