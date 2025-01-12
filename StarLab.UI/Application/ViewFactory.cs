using AutoMapper;
using Castle.Windsor;
using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;

namespace StarLab.Application
{
    /// <summary>
    /// A factory for creating  <see cref="IView"/>s.
    /// </summary>
    public class ViewFactory : Factory, IViewFactory
    {
        private readonly IConfigurationService configuration; // A service that provides the configuration information.

        private readonly IPresenterFactory factory; // This can be used to create use case presenters.

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewFactory"/> class.
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to resolve dependencies.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IConfigurationService"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ViewFactory(IPresenterFactory factory, IConfigurationService configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="name">The name of the configuration section.</param>
        /// <param name="view">The <see cref="IView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        public IPresenter CreatePresenter(string name, IView view)
        {
            return factory.CreatePresenter(name, view);
        }

        /// <summary>
        /// Creates an <see cref="IDockableViewPresenter"/> to control the <see cref="IDocumentView"/> provided.
        /// </summary>
        /// <param name="document">An <see cref="IDocument"/> that the view represents.</param>
        /// <param name="view">The <see cref="IDocumentView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IDockableViewPresenter"/> that can be used to control the <see cref="IDocumentView"/> provided.</returns>
        public IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view)
        {
            return factory.CreatePresenter(document, view);
        }

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IViewConfiguration"/> of the parent view.</param>
        /// <param name="child">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        public IChildViewPresenter CreatePresenter(IViewConfiguration parent, IChildView child)
        {
            return factory.CreatePresenter(parent, child);
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView CreateView(string name, string text)
        {
            return CreateView(configuration.GetViewConfiguration(name), text);
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"> that specifies the view to be created.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView CreateView(IDocument document)
        {
            return new DocumentView(document, this, configuration.GetViewConfiguration(document.View));
        }

        /// <summary>
        /// Creates the specified <see cref="IChildView"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IChildViewConfiguration"/> that specifies the view to be created.</param>
        /// <param name="parent">The <see cref="IViewConfiguration"/> of the parent view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        public IChildView CreateView(IChildViewConfiguration configuration, IViewConfiguration parent)
        {
            return (IChildView)CreateInstance(configuration.View, new object[] { configuration, parent, this });
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IViewConfiguration"/> that specifies the view to be created.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        /// <exception cref="Exception"></exception>
        private IView CreateView(IViewConfiguration configuration, string text)
        {
            IView view;

            switch (configuration.Type)
            {
                case ViewTypes.Application:
                    view = new WorkspaceView(Resources.StarLab, this);
                    break;

                case ViewTypes.Dialog:
                    view = new DialogView(configuration.Name, text, this, configuration);
                    break;

                case ViewTypes.Tool:
                    view = new ToolView(configuration.Name, text, this, configuration);
                    break;

                default:
                    throw new Exception(); // TODO
            }

            return view;
        }
    }
}
