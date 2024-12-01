using AutoMapper;
using Castle.Windsor;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;

namespace StarLab.Application
{
    public class PresenterFactory : Factory, IPresenterFactory
    {
        private readonly Dictionary<string, string> presenters = new Dictionary<string, string>();

        private readonly IWindsorContainer container;

        private readonly IUseCaseFactory useCaseFactory;

        private readonly IConfiguration configuration;

        private readonly IEventAggregator events;

        private readonly IMapper mapper;

        public PresenterFactory(IWindsorContainer container, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
        {
            this.useCaseFactory = useCaseFactory ?? throw new ArgumentNullException(nameof(useCaseFactory));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.events = events ?? throw new ArgumentNullException(nameof(events));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            Initialise();
        }

        public IChildViewPresenter CreatePresenter(IChildView view)
        {
            var commands = container.Resolve<ICommandManager>();

            if (view.Name == "ChartView") // TODO - This needs to be done properly. ChartView is generic - generate specific chart types by picking the appropriate presenter
            {
                return (IChildViewPresenter)CreateInstance(presenters[Views.CHART], new object[] { view, commands, useCaseFactory, configuration, mapper, events });
            }
            else
            {
                return (IChildViewPresenter)CreateInstance(presenters[view.Name], new object[] { view, commands, useCaseFactory, configuration, mapper, events });
            }

            
        }

        public IDockableViewPresenter CreatePresenter(IDockableView view)
        {
            var commands = container.Resolve<ICommandManager>();

            return new ToolViewPresenter(view, commands, useCaseFactory, configuration, mapper, events);
        }

        public IDockableViewPresenter CreatePresenter(IDocumentView view, IDocument document)
        {
            var commands = container.Resolve<ICommandManager>();

            return new DocumentViewPresenter(view, document, commands, useCaseFactory, configuration, mapper, events);
        }

        public IDialogViewPresenter CreatePresenter(IDialogView view)
        {
            var commands = container.Resolve<ICommandManager>();

            return new DialogViewPresenter(view, commands, useCaseFactory, configuration, mapper, events);
        }

        public IWorkspaceViewPresenter CreatePresenter(IWorkspaceView view)
        {
            var commands = container.Resolve<ICommandManager>();

            return new WorkspaceViewPresenter(view, commands, useCaseFactory, configuration, mapper, events);
        }

        private void Initialise()
        {
            presenters.Add(Views.ABOUT, "StarLab.Application.Help.AboutViewPresenter, StarLab.Presentation");
            presenters.Add(Views.ADD_DOCUMENT, "StarLab.Application.Workspace.Documents.AddDocumentViewPresenter, StarLab.Presentation");
            presenters.Add(Views.CHART_SETTINGS, "StarLab.Application.Workspace.Documents.Charts.ChartSettingsViewPresenter, StarLab.Presentation");
            presenters.Add(Views.CHART, "StarLab.Application.Workspace.Documents.Charts.ColourMagnitudeChartViewPresenter, StarLab.Presentation");
            presenters.Add(Views.OPTIONS, "StarLab.Application.Options.OptionsViewPresenter, StarLab.Presentation");
            presenters.Add(Views.WORKSPACE_EXPLORER, "StarLab.Application.Workspace.WorkspaceExplorer.WorkspaceExplorerViewPresenter, StarLab.Presentation");
        }
    }
}
