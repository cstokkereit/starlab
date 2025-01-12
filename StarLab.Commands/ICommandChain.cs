namespace StarLab.Commands
{
    /// <summary>
    /// Represents a chain of commands that will be executed in the order in which they were added to the chain.
    /// </summary>
    public interface ICommandChain : ICommand, IComponentCommand
    {
        /// <summary>
        /// Adds the <see cref="ICommand"/> provided to the chain.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> being added.</param>
        void Add(ICommand command);
    }
}
