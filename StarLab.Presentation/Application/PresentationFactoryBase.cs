using AutoMapper;
using Castle.Windsor;
using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Commands;

namespace StarLab.Application
{
    public abstract class PresentationFactoryBase : Factory, IPresentationFactory
    {
        private readonly IConfigurationService configuration;

        private readonly IWindsorContainer container;

        private readonly IEventAggregator events;

        private readonly IUseCaseFactory factory;

        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to resolve dependencies.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IConfigurationService"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PresentationFactoryBase(IWindsorContainer container, IUseCaseFactory factory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.events = events ?? throw new ArgumentNullException(nameof(events));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="name">The name of the configuration section.</param>
        /// <param name="view">The <see cref="IView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        public IPresenter CreatePresenter(string name, IView view)
        {
            return CreatePresenter(view, configuration.GetViewConfiguration(name));
        }

        /// <summary>
        /// Creates an <see cref="IDockableViewPresenter"/> to control the <see cref="IDocumentView"/> provided.
        /// </summary>
        /// <param name="document">An <see cref="IDocument"/> that </param>
        /// <param name="view">The <see cref="IDocumentView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IDockableViewPresenter"/> that can be used to control the <see cref="IDocumentView"/> provided.</returns>
        public IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view)
        {
            return new DocumentViewPresenter(view, document, container.Resolve<ICommandManager>(), factory, configuration, mapper, events);
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IViewConfiguration"/> of the parent view.</param>
        /// <param name="child">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
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

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public abstract IView CreateView(string name, string text);

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"> that specifies the view to be created.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public abstract IView CreateView(IDocument document);

        /// <summary>
        /// Creates the specified <see cref="IChildView"/>.
        /// </summary>
        /// <param name="config">The <see cref="IContentConfiguration"/> that specifies the view to be created.</param>
        /// <param name="parent">The <see cref="IViewConfiguration"/> of the parent view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        public abstract IChildView CreateView(IContentConfiguration config, IViewConfiguration parent);

        /// <summary>
        /// Gets the <see cref="IConfigurationService"/>
        /// </summary>
        protected IConfigurationService Configuration => configuration;

        private IChildViewPresenter CreatePresenter(IChildView view, IContentConfiguration configuration)
        {
            return (IChildViewPresenter)CreateInstance(configuration.Presenter, new object[] { view, container.Resolve<ICommandManager>(), factory, this.configuration, mapper, events });
        }

        private IPresenter CreatePresenter(IView view, IViewConfiguration configuration)
        {
            var commands = container.Resolve<ICommandManager>();

            IPresenter presenter;

            switch (configuration.Type)
            {
                case ViewTypes.Application:
                    presenter = new WorkspaceViewPresenter((IWorkspaceView)view, commands, factory, this.configuration, mapper, events);
                    break;

                case ViewTypes.Dialog:
                    presenter = new DialogViewPresenter((IDialogView)view, commands, factory, this.configuration, mapper, events);
                    break;

                case ViewTypes.Tool:
                    presenter = new ToolViewPresenter((IDockableView)view, commands, factory, this.configuration, mapper, events);
                    break;

                default:
                    throw new Exception(); // TODO
            }

            return presenter;
        }
    }
}
