namespace StarLab.Commands
{
    /// <summary>
    /// Represents a collection of commands that support undo and redo functionality.
    /// </summary>
    public interface IUndoStack
    {
        /// <summary>
        /// Adds a command that has just been executed to the undo stack.
        /// </summary>
        /// <param name="command">The command to be added.</param>
        void Add(IRevertableCommand command);

        /// <summary>
        /// Executes the command at the top of the redo stack and moves it to the undo stack.
        /// </summary>
        void Redo();

        /// <summary>
        /// Undoes the command at the top of the undo stack and moves it to the redo stack.
        /// </summary>
        void Undo();
    }
}
