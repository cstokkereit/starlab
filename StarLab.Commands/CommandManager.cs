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
        /// <param name="name">The name of the <see cref="ICommand"/>.</param>
        /// <param name="command">The <see cref="ICommand"/> being added.</param>
        public void AddCommand(string name, ICommand command)
        {
            if (commands.ContainsKey(name)) throw new ArgumentException(string.Format(Resources.MessageCommandExists, name));

            if (command == null) throw new ArgumentNullException(nameof(command));

            commands.Add(name, command);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ICommand"/> has already been added.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommand"/>.</param>
        /// <returns><see cref="true"/> if the specifed <see cref="ICommand"/> has already been added; <see cref="false"/> otherwise.</returns>
        public bool ContainsCommand(string name)
        {
            return commands.ContainsKey(name); // TODO Add unit tests
        }

        /// <summary>
        /// Gets the specified <see cref="ICommand"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommand"/>.</param>
        /// <returns>The specified <see cref="ICommand"/>.</returns>
        public ICommand GetCommand(string name)
        {
            if (!commands.ContainsKey(name)) throw new ArgumentException(string.Format(Resources.MessageCommandNotFound, name));

            return commands[name];
        }

        /// <summary>
        /// Gets the <see cref="ICommandInvoker"/> associated with the specified <see cref="Component"/>.
        /// </summary>
        /// <param name="instance">The <see cref="Component"/> instance.</param>
        /// <returns>The specified <see cref="ICommandInvoker"/>.</returns>
        public ICommandInvoker GetCommandInvoker(Component instance)
        {
            var type = instance.GetType().ToString();

            return invokers[type];
        }

        /// <summary>
        /// Registers an <see cref="ICommandInvoker"/> with the <see cref="ICommandManager"/>.
        /// </summary>
        /// <param name="invoker">The <see cref="ICommandInvoker"/> being registered.</param>
        public void RegisterCommandInvoker(ICommandInvoker invoker)
        {
            if (!invokers.ContainsKey(invoker.Type)) invokers.Add(invoker.Type, invoker);
        }

        /// <summary>
        /// Removes the specified <see cref="ICommand"/> from the collection of managed commands.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommand"/> being removed.</param>
        public void RemoveCommand(string name)
        {
            if (!commands.ContainsKey(name)) throw new ArgumentException(string.Format(Resources.MessageCommandNotFound, nameof(name)));

            commands.Remove(name);
        }
    }
}
