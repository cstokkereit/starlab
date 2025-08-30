namespace Stratosoft.Commands
{
    /// <summary>
    /// Represents a command that can be executed with arguments.
    /// </summary>
    /// <typeparam name="TReceiver">The type of receiver that this command acts on.</typeparam>
    /// <typeparam name="TArguments">The type of the arguments required by the command.</typeparam>
    public interface IParameterisedCommand<TReceiver, TArguments> : ICommand
    {
        /// <summary>
        /// Updates the receiver and then executes the command with the arguments provided when overridden in a derived class.
        /// </summary>
        /// <param name="receiver">The receiver that this command will act on.</param>
        /// <param name="arguments">The arguments required to execute this command.</param>
        void Execute(TReceiver receiver, TArguments arguments);

        /// <summary>
        /// Executes the command with the arguments provided.
        /// </summary>
        /// <param name="arguments">The arguments required to execute the command.</param>
        void Execute(TArguments arguments);
    }
}
