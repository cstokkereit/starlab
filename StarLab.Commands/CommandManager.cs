using StarLab.Commands.Properties;
using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for managing commands and their invokers.
    /// </summary>
    public class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>(); // A dictionary containing the commands indexed by name.

        private readonly Dictionary<string, ICommandInvoker> invokers = new Dictionary<string, ICommandInvoker>(); // A dictionary containing the command invokers indexed by component type.

        /// <summary>
        /// Adds an <see cref="ICommand"/> to the collection of managed commands.
        /// </summary>
        /// <param name="name">The name used to identify the command.</param>
        /// <param name="command">The <see cref="ICommand"/> being added.</param>
        public void AddCommand(string name, ICommand command)
        {
            if (commands.ContainsKey(name)) throw new ArgumentException(string.Format(Resources.MessageCommandExists, name));

            if (command == null) throw new ArgumentNullException("command");

            commands.Add(name, command);
        }

        /// <summary>
        /// Determines whether a command with the specified name has already been added.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>true if the specifed command has already been added; false otherwise.</returns>
        public bool ContainsCommand(string name)
        {
            return commands.ContainsKey(name); // TODO Add unit tests
        }

        /// <summary>
        /// Gets the <see cref="ICommand"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>The specified <see cref="ICommand"/>.</returns>
        public ICommand GetCommand(string name)
        {
            if (!commands.ContainsKey(name)) throw new ArgumentException(string.Format(Resources.MessageCommandNotFound, name));

            return commands[name];
        }

        /// <summary>
        /// Gets the <see cref="ICommandInvoker"/> associated with the specified component instance.
        /// </summary>
        /// <param name="instance">The component instance.</param>
        /// <returns>The specified <see cref="ICommandInvoker"/>.</returns>
        public ICommandInvoker GetCommandInvoker(Component instance)
        {
            var type = instance.GetType().ToString();

            return invokers[type];
        }

        /// <summary>
        /// Registers an <see cref="ICommandInvoker"/> with the <see cref="CommandManager"/>.
        /// </summary>
        /// <param name="invoker">The <see cref="ICommandInvoker"/> being registered.</param>
        public void RegisterCommandInvoker(ICommandInvoker invoker)
        {
            if (!invokers.ContainsKey(invoker.Type)) invokers.Add(invoker.Type, invoker);
        }

        /// <summary>
        /// Removes an <see cref="ICommand"/> from the collection of managed commands.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        public void RemoveCommand(string name)
        {
            if (!commands.ContainsKey(name)) throw new ArgumentException(string.Format(Resources.MessageCommandNotFound, name));

            commands.Remove(name);
        }
    }
}
