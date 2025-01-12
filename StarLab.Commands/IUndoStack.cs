namespace StarLab.Commands
{
    /// <summary>
    /// Represents a collection of <see cref="ICommand"/>s that supports undo and redo functionality.
    /// </summary>
    public interface IUndoStack
    {
        /// <summary>
        /// Adds an <see cref="ICommand"/> that has just been executed to the undo stack.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> to be added.</param>
        void Add(IRevertableCommand command);

        /// <summary>
        /// Executes the <see cref="ICommand"/> at the top of the redo stack and moves it to the undo stack.
        /// </summary>
        void Redo();

        /// <summary>
        /// Undoes the <see cref="ICommand"/> at the top of the undo stack and moves it to the redo stack.
        /// </summary>
        void Undo();
    }
}
