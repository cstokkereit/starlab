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
        /// Initiates the workflow managed by the dialog box.
        /// </summary>
        /// <param name="context">An <see cref="IWorkflowContext"/> that contains the information required to execute the workflow.</param>
        void Run(IWorkflowContext context);
    }
}
