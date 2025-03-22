using AutoMapper;
using Castle.Windsor;
using StarLab.Application;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// A factory for creating <see cref="IPresenter"/>s.
    /// </summary>
    public class PresenterFactory : Factory, IPresenterFactory
    {
        private readonly Configuration.IConfigurationProvider configuration; // A service that provides the configuration information.

        private readonly IWindsorContainer container; // Used to resolve dependencies at run time.

        private readonly IEventAggregator events; // This can be used for subscribing to and publishing events.

        private readonly IUseCaseFactory factory; // This can be used to create use case interactors.

        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        /// <summary>
        /// Initialises a new instance of the <see cref="PresenterFactory"> class.
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to resolve dependencies.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IConfigurationProvider"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PresenterFactory(IWindsorContainer container, IUseCaseFactory factory, Configuration.IConfigurationProvider configuration, IMapper mapper, IEventAggregator events)
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
        /// <param name="document">An <see cref="IDocument"/> that the view represents.</param>
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

            if (parent.ChildViews.Count > 1)
            {
                presenter = CreatePresenter(child, parent.GetChildViewConfiguration(child.Name));
            }
            else
            {
                presenter = CreatePresenter(child, parent.ChildViews[0]);
            }

            return presenter;
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <param name="configuration">The <see cref="IChildViewConfiguration"/> for the child view.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        private IChildViewPresenter CreatePresenter(IChildView view, IChildViewConfiguration configuration)
        {
            return (IChildViewPresenter)CreateInstance(configuration.Presenter, new object[] { view, container.Resolve<ICommandManager>(), factory, this.configuration, mapper, events });
        }

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that the presenter will control.</param>
        /// <param name="configuration">The <see cref="IViewConfiguration"/> for the view.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        /// <exception cref="Exception"></exception>
        private IPresenter CreatePresenter(IView view, IViewConfiguration configuration)
        {
            var commands = container.Resolve<ICommandManager>();

            IPresenter presenter;

            switch (configuration.Type)
            {
                case ViewTypes.Application:
                    presenter = new ApplicationViewPresenter((IApplicationView)view, commands, factory, this.configuration, mapper, events);
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
