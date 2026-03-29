namespace Stratosoft.Commands
{
    /// <summary>
    /// A simple implementation of the <see cref="ICommand"/> interface that executes the specified parameterless <see cref="Action"/> when the command is executed.
    /// </summary>
    public class ActionCommand : ICommand
    {
        private readonly Action action; // The action to perform when the command is executed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ActionCommand"/>.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to perform when the command is executed.</param>
        public ActionCommand(Action action)
        {
            this.action = action; 
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            action();
        }
    }
}
