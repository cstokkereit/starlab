namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a controller that can be used to display commonly used dialogs.
    /// </summary>
    public interface IViewController : IController
    {
        /// <summary>
        /// Initialises the <see cref="IViewController"/>.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/> that manages the views that comprise the user interface of the application.</param>
        void Initialise(IApplicationController controller);

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        void Show(IView view);
    }
}
