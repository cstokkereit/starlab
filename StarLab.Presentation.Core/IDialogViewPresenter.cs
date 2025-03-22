using System.ComponentModel;

namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the methods used by an <see cref="IDialogView"/> to communicate with its presenter.
    /// </summary>
    public interface IDialogViewPresenter : IPresenter
    {
        /// <summary>
        /// Notifies the presenter that the view is being closed.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs"/> that can be used to determine the reasons that the view is closing and, if necessary, cancel it.</param>
        void ViewClosing(CancelEventArgs e);
    }
}
