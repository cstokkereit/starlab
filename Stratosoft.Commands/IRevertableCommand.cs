namespace Stratosoft.Commands
{
    /// <summary>
    /// Represents a command that supports undo and redo functionality.
    /// </summary>
    public interface IRevertableCommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        void Redo();

        /// <summary>
        /// Reverses the effects of executing the command.
        /// </summary>
        void Undo();
    }
}
