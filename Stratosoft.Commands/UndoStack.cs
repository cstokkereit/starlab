namespace Stratosoft.Commands
{
    /// <summary>
    /// A basic implementation of the <see cref="IUndoStack"/> interface.
    /// </summary>
    public class UndoStack : IUndoStack
    {
        private readonly Stack<IRevertableCommand> redo = new Stack<IRevertableCommand>(); // A stack containing the commands that have been reverted.

        private readonly Stack<IRevertableCommand> undo = new Stack<IRevertableCommand>(); // A stack containing the commands that have been executed.

        /// <summary>
        /// Gets the depth of the redo stack.
        /// </summary>
        public int RedoCount { get { return redo.Count; } }

        /// <summary>
        /// Gets the depth of the undo stack.
        /// </summary>
        public int UndoCount { get { return undo.Count; } }

        /// <summary>
        /// Adds an <see cref="ICommand"/> that has just been executed to the undo stack.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> to be added.</param>
        public virtual void Add(IRevertableCommand command)
        {
            undo.Push(command);
            redo.Clear();
        }

        /// <summary>
        /// Executes the <see cref="ICommand"/> at the top of the redo stack and moves it to the undo stack.
        /// </summary>
        public virtual void Redo()
        {
            var command = redo.Pop();
            command.Redo();
            undo.Push(command);
        }

        /// <summary>
        /// Undoes the <see cref="ICommand"/> at the top of the undo stack and moves it to the redo stack.
        /// </summary>
        public virtual void Undo()
        {
            var command = undo.Pop();
            command.Undo();
            redo.Push(command);
        }
    }
}
