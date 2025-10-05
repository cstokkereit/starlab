using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a factory for creating views.
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="name">The name of the view.</param>
        /// <param name="text">The view text.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        IView CreateView(string name, string text);

        /// <summary>
        /// Creates the specified <see cref="IView"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"> that specifies the view to be created.</param>
        /// <returns>The specified <see cref="IView"/>.</returns>
        IView CreateView(IDocument document);
    }
}
