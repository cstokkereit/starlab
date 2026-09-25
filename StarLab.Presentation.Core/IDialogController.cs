namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a controller that controls the behaviour of a dialog box.
    /// </summary>
    public interface IDialogController : IViewController
    {
        /// <summary>
        /// Closes the dialog box.
        /// </summary>
        void Close();

        /// <summary>
        /// Runs the dialog view.
        /// </summary>
        /// <param name="args">An <see cref="INamedArguments"/> that contains information required to run the view.</param>
        void Run(INamedArguments args);
    }
}
