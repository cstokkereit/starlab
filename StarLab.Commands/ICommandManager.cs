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
        /// <param name="name">The name of the <see cref="ICommand"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> being added.</param>
        void AddCommand(string name, ICommand command);

        /// <summary>
        /// Determines whether the specified <see cref="ICommand"/> has already been added.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommand"/>.</param>
        /// <returns><see cref="true"/> if the specifed <see cref="ICommand"/> has already been added; <see cref="false"/> otherwise.</returns>
        bool ContainsCommand(string name);

        /// <summary>
        /// Gets the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommand"/>.</param>
        /// <returns>The specified <see cref="ICommand"/>.</returns>
        ICommand GetCommand(string name);

        /// <summary>
        /// Gets the <see cref="ICommandInvoker"/> associated with the specified <see cref="Component"/>.
        /// </summary>
        /// <param name="instance">The <see cref="Component"/> instance.</param>
        /// <returns>The specified <see cref="ICommandInvoker"/>.</returns>
        ICommandInvoker GetCommandInvoker(Component instance);

        /// <summary>
        /// Registers an <see cref="ICommandInvoker"/> with the <see cref="ICommandManager"/>.
        /// </summary>
        /// <param name="invoker">The <see cref="ICommandInvoker"/> being registered.</param>
        void RegisterCommandInvoker(ICommandInvoker invoker);

        /// <summary>
        /// Removes the specified <see cref="ICommand"/> from the collection of managed commands.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommand"/> being removed.</param>
        void RemoveCommand(string name);
    }
}
