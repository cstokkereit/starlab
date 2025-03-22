using StarLab.Presentation.Workspace;

namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the methods used by the <see cref="IApplicationView"/> to communicate with its presenter.
    /// </summary>
    public interface IApplicationViewPresenter : IDialogViewPresenter
    {
        /// <summary>
        /// Clears the active document.
        /// </summary>
        void ClearActiveDocument();

        /// <summary>
        /// Returns the <see cref="IDockableView"/> with the specified ID if it exists. If not, a new <see cref="IDockableView"/> with the specified ID will be created.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IDockableView"/>.</param>
        /// <returns>The <see cref="IDockableView"/> with the specified ID.</returns>
        IDockableView? CreateView(string id);

        /// <summary>
        /// Makes the document with the specified ID the active document.
        /// </summary>
        /// <param name="id">The ID of the document.</param>
        void SetActiveDocument(string id);
    }
}
