using log4net;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// A factory for creating presenters.
    /// </summary>
    public class PresenterFactory : Factory, IPresenterFactory
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PresenterFactory)); // The logger that will be used for writing log messages.

        private readonly IApplicationSettings settings; // Provides access to the application configuration.

        private readonly IEventAggregator events; // This can be used for subscribing to and publishing events.

        private readonly IServiceRegistry services; // Provides access to the available services.

        /// <summary>
        /// Initialises a new instance of the <see cref="PresenterFactory"> class.
        /// </summary>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the available services.</param>
        /// <param name="configuration">The type configuration information.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application settings.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PresenterFactory(IServiceRegistry services, IFactoryConfiguration configuration, IApplicationSettings settings, IEventAggregator events)
            : base(configuration)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.events = events ?? throw new ArgumentNullException(nameof(events));
        }

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that the presenter controls.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        /// <exception cref="ArgumentException"></exception>
        public IPresenter CreatePresenter(IView view, ICommandManager commands)
        {
            if (view is IApplicationView application)
            {
                return new ApplicationViewPresenter(application, commands, services, settings, events);
            }

            throw new ArgumentException(string.Format(Resources.UnexpectedViewType, view.GetType().Name), nameof(view));
        }

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that the presenter controls.</param>
        /// <param name="childPresenter">The <see cref="IChildViewPresenter"/> that controls the child view.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        /// <exception cref="ArgumentException"></exception>
        public IPresenter CreatePresenter(IView view, IChildViewPresenter childPresenter, ICommandManager commands)
        {
            if (childPresenter is IChildViewController childController)
            {
                if (view is IDialogView dialog)
                {
                    return new DialogViewPresenter(dialog, childController, commands, settings, events);
                }
                else if (view is IDockableView docakable)
                {
                    return new ToolViewPresenter(docakable, childController, commands, settings, events);
                }
            }
            else
            {
                throw new ArgumentException(string.Format(Resources.InterfaceNotImplemented, nameof(childPresenter), nameof(IChildViewController)));
            }

            throw new ArgumentException(string.Format(Resources.UnexpectedViewType, view.GetType().Name), nameof(view));
        }

        /// <summary>
        /// Creates an <see cref="IDockableViewPresenter"/> to control the <see cref="IDocumentView"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that the view represents.</param>
        /// <param name="view">The <see cref="IDocumentView"/> that the presenter will control.</param>
        /// <param name="childPresenters">An <see cref="IEnumerable{IChildViewPresenter}"/> containing the presenters that control the child views.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IDockableViewPresenter"/> that can be used to control the <see cref="IDocumentView"/> provided.</returns>
        /// <exception cref="Exception"></exception>
        public IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view, IEnumerable<IChildViewPresenter> childPresenters, ICommandManager commands)
        {
            var controllers = new List<IChildViewController>();

            foreach (var childPresenter in childPresenters)
            {
                if (childPresenter is IChildViewController controller)
                {
                    controllers.Add(controller);
                }
                else
                {
                    throw new Exception(string.Format(Resources.InterfaceNotImplemented, nameof(childPresenter), nameof(IChildViewController)));
                }
            }

            return new DocumentViewPresenter(view, document, controllers, commands, settings, events);
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="child">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        public IChildViewPresenter CreatePresenter(IChildView view, ICommandManager commands)
        {
            var configuration = GetViewConfiguration(view.Name);

            return (IChildViewPresenter)CreateInstance(configuration.GetChildViewConfiguration(view.Name).Presenter, [view, commands, services, settings, events]);
        }

        /// <summary>
        /// Gets the child view presenters for the specified parent view.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that specifies the parent view.</param>
        /// <param name="views">An <see cref="IEnumerable{IChildView}"/> containing the child views.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IEnumerable{IChildViewPresenter}"/> containing the child view presenters.</returns>
        public IEnumerable<IChildViewPresenter> CreatePresenters(IDocument document, IEnumerable<IChildView> views, ICommandManager commands)
        {
            var presenters = new List<IChildViewPresenter>();

            var configuration = GetViewConfiguration(document.View);

            foreach (var view in views)
            {
                presenters.Add((IChildViewPresenter)CreateInstance(configuration.GetChildViewConfiguration(view.Name).Presenter, [view, commands, services, settings, events]));
            }

            return presenters;
        }
    }
}
