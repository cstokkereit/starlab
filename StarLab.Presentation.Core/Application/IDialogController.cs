namespace StarLab.Application
{
    /// <summary>
    /// Represents a controller that can be used to control a dialog box.
    /// </summary>
    public interface IDialogController : IViewController
    {
        /// <summary>
        /// Closes the dialog box.
        /// </summary>
        void Close();

        /// <summary>
        /// Shows the dialog box.
        /// </summary>
        void Show();
    }
}
