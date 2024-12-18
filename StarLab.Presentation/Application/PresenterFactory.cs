using AutoMapper;
using Castle.Windsor;
using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;

namespace StarLab.Application
{
    public class PresenterFactory : Factory, IPresenterFactory
    {
        private readonly IConfigurationService configService;

        private readonly IWindsorContainer container;

        private readonly IEventAggregator events;

        private readonly IUseCaseFactory factory;

        private readonly IMapper mapper;

        public PresenterFactory(IWindsorContainer container, IUseCaseFactory factory, IConfigurationService configService, IMapper mapper, IEventAggregator events)
        {
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.events = events ?? throw new ArgumentNullException(nameof(events));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IPresenter CreatePresenter(string name, IView view)
        {
            return CreatePresenter(view, configService.GetViewConfiguration(name));
        }

        public IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view)
        {
            return new DocumentViewPresenter(view, document, container.Resolve<ICommandManager>(), factory, configService, mapper, events);
        }

        public IChildViewPresenter CreatePresenter(IViewConfiguration parent, IChildView child)
        {
            IChildViewPresenter presenter;

            if (parent.Contents.Count > 1)
            {
                presenter = CreatePresenter(child, parent.GetContentConfiguration(child.Name));
            }
            else
            {
                presenter = CreatePresenter(child, parent.Contents[0]);
            }

            return presenter;
        }

        private IChildViewPresenter CreatePresenter(IChildView view, IContentConfiguration configuration)
        {
            return (IChildViewPresenter)CreateInstance(configuration.Presenter, new object[] { view, container.Resolve<ICommandManager>(), factory, configService, mapper, events });
        }

        private IPresenter CreatePresenter(IView view, IViewConfiguration configuration)
        {
            var commands = container.Resolve<ICommandManager>();

            IPresenter presenter;

            switch (configuration.Type)
            {
                case ViewTypes.Application:
                    presenter = new WorkspaceViewPresenter((IWorkspaceView)view, commands, factory, configService, mapper, events);
                    break;

                case ViewTypes.Dialog:
                    presenter = new DialogViewPresenter((IDialogView)view, commands, factory, configService, mapper, events);
                    break;

                case ViewTypes.Tool:
                    presenter = new ToolViewPresenter((IDockableView)view, commands, factory, configService, mapper, events);
                    break;

                default:
                    throw new Exception(); // TODO
            }

            return presenter;
        }
    }
}
