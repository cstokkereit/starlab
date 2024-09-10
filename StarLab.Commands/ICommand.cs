namespace StarLab.Commands
{
    /// <summary>
    /// Represents a command that can be executed without needing any arguments.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();
    }
}
