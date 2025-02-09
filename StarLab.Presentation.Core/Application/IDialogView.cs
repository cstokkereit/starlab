namespace StarLab.Application
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IDialogViewPresenter"/> to control the behaviour of a dialog box.
    /// </summary>
    public interface IDialogView : IView
    {
        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);
    }
}
