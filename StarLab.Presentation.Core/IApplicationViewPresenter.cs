using StarLab.Presentation.Workspace;
using System.ComponentModel;

namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the methods used by the <see cref="IApplicationView"/> to communicate with its presenter.
    /// </summary>
    public interface IApplicationViewPresenter : IPresenter
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

        /// <summary>
        /// Notifies the presenter that the view has been activated.
        /// </summary>
        void ViewActivated();

        /// <summary>
        /// Notifies the presenter that the view is being closed.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs"/> that can be used to determine the reasons that the view is closing and, if necessary, cancel it.</param>
        void ViewClosing(CancelEventArgs e);
    }
}
