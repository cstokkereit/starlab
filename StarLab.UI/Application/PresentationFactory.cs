using AutoMapper;
using Castle.Windsor;
using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;

namespace StarLab.Application
{
    /// <summary>
    /// A factory for creating presenters and views.
    /// </summary>
    public class PresentationFactory : PresentationFactoryBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PresentationFactory"/> class.
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to resolve dependencies.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IConfigurationService"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public PresentationFactory(IWindsorContainer container, IUseCaseFactory factory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(container, factory, configuration, mapper, events) { }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public override IView CreateView(string name, string text)
        {
            return CreateView(Configuration.GetViewConfiguration(name), text);
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"> that specifies the view to be created.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public override IView CreateView(IDocument document)
        {
            return new DocumentView(document, this, Configuration.GetViewConfiguration(document.View));
        }

        /// <summary>
        /// Creates the specified <see cref="IChildView"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IContentConfiguration"/> that specifies the view to be created.</param>
        /// <param name="parent">The <see cref="IViewConfiguration"/> of the parent view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        public override IChildView CreateView(IContentConfiguration configuration, IViewConfiguration parent)
        {
            return (IChildView)CreateInstance(configuration.View, new object[] { configuration, parent, this });
        }

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IContentConfiguration"/> that specifies the view to be created.</param>
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
