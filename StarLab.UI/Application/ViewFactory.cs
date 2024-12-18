using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    /// <summary>
    /// A class that is used to create instances of <see cref="IView"/> and <see cref="IChildView"/>.
    /// </summary>
    public class ViewFactory : Factory, IViewFactory
    {
        private readonly IConfigurationService configService;

        private readonly IPresenterFactory factory;

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewFactory"/> class.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="configService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ViewFactory(IPresenterFactory factory, IConfigurationService configService)
        {
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public IPresenter CreatePresenter(string name, IView view)
        {
            return factory.CreatePresenter(name, view);
        }

        public IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view)
        {
            return factory.CreatePresenter(document, view);
        }

        public IChildViewPresenter CreatePresenter(IViewConfiguration parent, IChildView child)
        {
            return factory.CreatePresenter(parent, child);
        }

        public IView CreateView(string name, string text)
        {
            return CreateView(configService.GetViewConfiguration(name), text);
        }

        public IView CreateView(IDocument document)
        {
            return new DocumentView(document, this, configService.GetViewConfiguration(document.View));
        }

        public IChildView CreateView(IContentConfiguration config, IViewConfiguration parent)
        {
            return (IChildView)CreateInstance(config.View, new object[] { config, parent, this });
        }

        private IView CreateView(IViewConfiguration config, string text)
        {
            IView view;

            switch (config.Type)
            {
                case ViewTypes.Application:
                    view = new WorkspaceView(this);
                    break;

                case ViewTypes.Dialog:
                    view = new DialogView(config.Name, text, this, config);
                    break;

                case ViewTypes.Tool:
                    view = new ToolView(config.Name, text, this, config);
                    break;

                default:
                    throw new Exception(); // TODO
            }

            return view;
        }
    }
}
