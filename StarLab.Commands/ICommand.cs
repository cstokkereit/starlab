namespace StarLab.Commands
{
    /// <summary>
    /// Represents a command that can be executed without any arguments.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();
    }
}
