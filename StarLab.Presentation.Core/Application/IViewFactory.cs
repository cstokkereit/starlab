using StarLab.Application.Configuration;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    /// <summary>
    /// Represents a factory for creating views.
    /// </summary>
    public interface IViewFactory : IPresenterFactory
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

        /// <summary>
        /// Creates the specified <see cref="IChildView"/>.
        /// </summary>
        /// <param name="config">The <see cref="IContentConfiguration"/> that specifies the view to be created.</param>
        /// <param name="parent">The <see cref="IViewConfiguration"/> of the parent view.</param>
        /// <returns>The specified <see cref="IChildView"/>.</returns>
        IChildView CreateView(IContentConfiguration config, IViewConfiguration parent);
    }
}
