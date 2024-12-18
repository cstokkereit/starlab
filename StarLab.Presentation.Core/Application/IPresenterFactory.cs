using StarLab.Application.Configuration;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    /// <summary>
    /// Represents a factory for creating presenters.
    /// </summary>
    public interface IPresenterFactory
    {
        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="name">The name of the configuration section.</param>
        /// <param name="view">The <see cref="IView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        IPresenter CreatePresenter(string name, IView view);

        /// <summary>
        /// Creates an <see cref="IDockableViewPresenter"/> to control the <see cref="IDocumentView"/> provided.
        /// </summary>
        /// <param name="document">An <see cref="IDocument"/> that </param>
        /// <param name="view">The <see cref="IDocumentView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IDockableViewPresenter"/> that can be used to control the <see cref="IDocumentView"/> provided.</returns>
        IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view);

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IViewConfiguration"/> of the parent view.</param>
        /// <param name="child">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        IChildViewPresenter CreatePresenter(IViewConfiguration parent, IChildView child);
    }
}
