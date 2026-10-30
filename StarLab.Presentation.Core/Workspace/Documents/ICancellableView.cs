using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Defines the methods used attach the commands that will be executed when the OK and Cancel buttons are clicked.
    /// </summary>
    public interface ICancellableView
    {
        /// <summary>
        /// Attaches the command provided to the Cancel button.
        /// </summary>
        /// <param name="command">The command that will be executed when the Cancel button is clicked.</param>
        void AttachCancelButtonCommand(ICommand command);

        /// <summary>
        /// Attaches the command provided to the OK button.
        /// </summary>
        /// <param name="command">The command that will be executed when the OK button is clicked.</param>
        void AttachOKButtonCommand(ICommand command);
    }
}
