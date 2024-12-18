namespace StarLab.Application
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IDialogViewPresenter"/> to control the behaviour of a dialog box.
    /// </summary>
    public interface IDialogView : IView
    {
        /// <summary>
        /// Gets or sets a flag that determines whether the dialog box will be hidden or unloaded when it is closed.
        /// </summary>
        bool HideOnClose { get; set; }

        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);
    }
}
