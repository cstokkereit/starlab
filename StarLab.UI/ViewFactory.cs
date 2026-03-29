using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents;
using StarLab.UI.Workspace;
using StarLab.UI.Workspace.Documents;

namespace StarLab.UI
{
    /// <summary>
    /// A factory for creating views.
    /// </summary>
    public class ViewFactory : Factory, IViewFactory
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ViewFactory)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewFactory"/> class.
        /// </summary>
        /// <param name="configuration">The type configuration information.</param>
        public ViewFactory(IFactoryConfiguration configuration)
            : base(configuration) { }

        /// <summary>
        /// Creates the application view.
        /// </summary>
        /// <param name="text">The view text.</param>
        /// <returns>The application <see cref="IView"/>.</returns>
        public IView CreateApplicationView(string text)
        {
            return new ApplicationView(text);
        }

        /// <summary>
        /// Creates the specified child view.
        /// </summary>
        /// <param name="parent">The name of the parent view.</param>
        /// <param name="name">The name of the child view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        public IChildView CreateChildView(string parent, string name)
        {
            var configuration = GetViewConfiguration(parent);

            return (IChildView)CreateInstance(configuration.GetChildViewConfiguration(name).View);
        }

        /// <summary>
        /// Gets the child views for the specified parent view.
        /// </summary>
        /// <param name="name">The name of the parent view.</param>
        /// <returns>An <see cref="IEnumerable{IChildView}"/> containing the child views.</returns>
        public IEnumerable<IChildView> CreateChildViews(string name)
        {
            var views = new List<IChildView>();

            var configuration = GetViewConfiguration(name);

            foreach (var childConfiguration in configuration.ChildConfigurations)
            {
                views.Add((IChildView)CreateInstance(childConfiguration.View));
            }

            return views;
        }

        /// <summary>
        /// Gets the child views for the specified parent view.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that specifies the parent view.</param>
        /// <returns>An <see cref="IEnumerable{IChildView}"/> containing the child views.</returns>
        public IEnumerable<IChildView> CreateChildViews(IDocument document)
        {
            var views = new List<IChildView>();

            var configuration = GetViewConfiguration(document.View);

            foreach (var childConfiguration in configuration.ChildConfigurations)
            {
                views.Add((IChildView)CreateInstance(childConfiguration.View));
            }

            return views;
        }

        /// <summary>
        /// Creates the specified dialog view.
        /// </summary>
        /// <param name="name">The name of the dialog.</param>
        /// <param name="text">The caption text.</param>
        /// <param name="childView">The <see cref="IChildView"/> the provides the dialog functionality.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView CreateDialogView(string name, string text, IChildView childView)
        {
            return new DialogView(name, text, childView);
        }

        /// <summary>
        /// Creates the specified dialog view.
        /// </summary>
        /// <param name="name">The name of the dialog.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView CreateDialogView(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the specified tool window.
        /// </summary>
        /// <param name="name">The name of the tool window.</param>
        /// <param name="text">The caption text.</param>
        /// <param name="childView">The <see cref="IChildView"/> the provides the tool window functionality.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        public IView CreateToolView(string name, string text, IChildView childView)
        {
            return new ToolView(name, text, childView);
        }

        /// <summary>
        /// Creates the specified <see cref="IDocumentView"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that specifies the view to be created.</param>
        /// <param name="childViews">An <see cref="IEnumerable{IChildView}"/> containing the child views that provide the document view functionality.</param>
        /// <returns>The specified <see cref="IDocumentView"/>.</returns>
        public IDocumentView CreateView(IDocument document, IEnumerable<IChildView> childViews)
        {
            return new DocumentView(document, childViews);
        }
    }
}
