using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLab.Commands
{
    /// <summary>
    /// A basic implementation of the <see cref="IUndoStack"/> class.
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
        /// Adds a command that has just been executed to the undo stack.
        /// </summary>
        /// <param name="command">The command to be added.</param>
        public virtual void Add(IRevertableCommand command)
        {
            undo.Push(command);
            redo.Clear();
        }

        /// <summary>
        /// Executes the command at the top of the redo stack and moves it to the undo stack.
        /// </summary>
        public virtual void Redo()
        {
            var command = redo.Pop();
            command.Redo();
            undo.Push(command);
        }

        /// <summary>
        /// Undoes the command at the top of the undo stack and moves it to the redo stack.
        /// </summary>
        public virtual void Undo()
        {
            var command = undo.Pop();
            command.Undo();
            redo.Push(command);
        }
    }
}
