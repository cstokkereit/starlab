using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a factory for creating presenters.
    /// </summary>
    public interface IPresenterFactory
    {
        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that the presenter controls.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        IPresenter CreatePresenter(IView view, ICommandManager commands);

        /// <summary>
        /// Creates an <see cref="IPresenter"/> to control the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> that the presenter controls.</param>
        /// <param name="childPresenter">The <see cref="IChildViewPresenter"/> that controls the child view.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IPresenter"/> that can be used to control the <see cref="IView"/> provided.</returns>
        IPresenter CreatePresenter(IView view, IChildViewPresenter childPresenter, ICommandManager commands);

        /// <summary>
        /// Creates an <see cref="IDockableViewPresenter"/> to control the <see cref="IDocumentView"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that the view represents.</param>
        /// <param name="view">The <see cref="IDocumentView"/> that the presenter will control.</param>
        /// <param name="childPresenters">An <see cref="IEnumerable{IChildViewPresenter}"/> containing the presenters that control the child views.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IDockableViewPresenter"/> that can be used to control the <see cref="IDocumentView"/> provided.</returns>
        IDockableViewPresenter CreatePresenter(IDocument document, IDocumentView view, IEnumerable<IChildViewPresenter> childPresenters, ICommandManager commands);

        /// <summary>
        /// Creates an <see cref="IChildViewPresenter"/> to control the <see cref="IChildView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IChildView"/> that the presenter will control.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IChildViewPresenter"/> that can be used to control the <see cref="IChildView"/> provided.</returns>
        IChildViewPresenter CreatePresenter(IChildView view, ICommandManager commands);

        /// <summary>
        /// Gets the child view presenters for the specified parent view.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that specifies the parent view.</param>
        /// <param name="views">An <see cref="IEnumerable{IChildView}"/> containing the child views.</param>
        /// <param name="commands">The <see cref="ICommandManager"/> that manages the commands.</param>
        /// <returns>An <see cref="IEnumerable{IChildViewPresenter}"/> containing the child view presenters.</returns>
        IEnumerable<IChildViewPresenter> CreatePresenters(IDocument document, IEnumerable<IChildView> views, ICommandManager commands);
    }
}
