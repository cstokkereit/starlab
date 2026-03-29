using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a factory for creating views.
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// Creates the application view.
        /// </summary>
        /// <param name="text">The view text.</param>
        /// <returns>The application <see cref="IView"/>.</returns>
        IView CreateApplicationView(string text);

        /// <summary>
        /// Creates the specified child view.
        /// </summary>
        /// <param name="parent">The name of the parent view.</param>
        /// <param name="name">The name of the child view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        IChildView CreateChildView(string parent, string name);

        /// <summary>
        /// Gets the child views for the specified parent view.
        /// </summary>
        /// <param name="name">The name of the parent view.</param>
        /// <returns>An <see cref="IEnumerable{IChildView}"/> containing the child views.</returns>
        IEnumerable<IChildView> CreateChildViews(string name);

        /// <summary>
        /// Gets the child views for the specified parent view.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that specifies the parent view.</param>
        /// <returns>An <see cref="IEnumerable{IChildView}"/> containing the child views.</returns>
        IEnumerable<IChildView> CreateChildViews(IDocument document);

        /// <summary>
        /// Creates the specified dialog view.
        /// </summary>
        /// <param name="name">The name of the dialog.</param>
        /// <param name="text">The caption text.</param>
        /// <param name="childView">The <see cref="IChildView"/> the provides the dialog functionality.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        IView CreateDialogView(string name, string text, IChildView childView);

        /// <summary>
        /// Creates the specified dialog view.
        /// </summary>
        /// <param name="name">The name of the dialog.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        IView CreateDialogView(string name);

        /// <summary>
        /// Creates the specified tool window.
        /// </summary>
        /// <param name="name">The name of the tool window.</param>
        /// <param name="text">The caption text.</param>
        /// <param name="childView">The <see cref="IChildView"/> the provides the tool window functionality.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        IView CreateToolView(string name, string text, IChildView childView);

        /// <summary>
        /// Creates the specified <see cref="IDocumentView"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that specifies the view to be created.</param>
        /// <param name="childViews">An <see cref="IEnumerable{IChildView}"/> containing the child views that provide the document view functionality.</param>
        /// <returns>The specified <see cref="IDocumentView"/>.</returns>
        IDocumentView CreateView(IDocument document, IEnumerable<IChildView> childViews);
    }
}
