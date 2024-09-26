namespace StarLab.Commands
{
    /// <summary>
    /// Represents a command that can be executed with arguments.
    /// </summary>
    /// <typeparam name="TArguments">The type of the arguments required by this command.</typeparam>
    public interface IParameterisedCommand<TArguments>
    {
        /// <summary>
        /// Executes the command with the arguments provided.
        /// </summary>
        /// <param name="arguments">The arguments required to execute this command.</param>
        void Execute(TArguments arguments);
    }
}
