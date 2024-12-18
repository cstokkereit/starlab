using System.ComponentModel;

namespace StarLab.Application
{
    /// <summary>
    /// Defines the methods used by an <see cref="IDialogView"/> to communicate with its presenter.
    /// </summary>
    public interface IDialogViewPresenter : IPresenter
    {
        /// <summary>
        /// This method is called in response to the view being closed.
        /// </summary>
        /// <param name="args">The <see cref="CancelEventArgs"/> that can be used to determine the reasons that the view is closing and, if necessary, cancel it.</param>
        void ViewClosing(CancelEventArgs args);
    }
}
