namespace StarLab.Application
{
    /// <summary>
    /// Represents a dialog box used to retrieve user input.
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// Shows the dialog with the specified <see cref="IInteractionContext"/>.
        /// </summary>
        /// <param name="context">An <see cref="IInteractionContext"/> that provides the context required to configure the dialog for a specific user interaction.</param>
        void Show(IInteractionContext context);
    }
}
