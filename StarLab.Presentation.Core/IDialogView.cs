namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IDialogViewPresenter"/> to control the behaviour of a dialog box.
    /// </summary>
    public interface IDialogView : IParentView
    {
        /// <summary>
        /// Closes the dialog box.
        /// </summary>
        void Close();
    }
}
