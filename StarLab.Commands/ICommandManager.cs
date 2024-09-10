using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// An interface that can be implemented by a class that manages commands and their invokers.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Adds an <see cref="ICommand"/> to the collection of managed commands.
        /// </summary>
        /// <param name="name">The name used to identify the command.</param>
        /// <param name="command">The <see cref="ICommand"/> being added.</param>
        void AddCommand(string name, ICommand command);

        /// <summary>
        /// Determines whether a command with the specified name has already been added.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>true if the specifed command has already been added; false otherwise.</returns>
        bool ContainsCommand(string name);

        /// <summary>
        /// Gets the <see cref="ICommand"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>The specified <see cref="ICommand"/>.</returns>
        ICommand GetCommand(string name);

        /// <summary>
        /// Gets the <see cref="ICommandInvoker"/> associated with the specified component instance.
        /// </summary>
        /// <param name="instance">The component instance.</param>
        /// <returns>The specified <see cref="ICommandInvoker"/>.</returns>
        ICommandInvoker GetCommandInvoker(Component instance);

        /// <summary>
        /// Registers an <see cref="ICommandInvoker"/> with the <see cref="ICommandManager"/>.
        /// </summary>
        /// <param name="invoker">The <see cref="ICommandInvoker"/> being registered.</param>
        void RegisterCommandInvoker(ICommandInvoker invoker);

        /// <summary>
        /// Removes an <see cref="ICommand"/> from the collection of managed commands.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        void RemoveCommand(string name);
    }
}
