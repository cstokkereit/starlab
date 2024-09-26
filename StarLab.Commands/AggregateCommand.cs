using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// Holds a collection of commands that will be executed sequentially when this command is executed.
    /// </summary>
    public class AggregateCommand : ICommand, IComponentCommand
    {

        // TODO - Change to Chain of command pattern

        protected readonly List<ICommand> commands = new List<ICommand>(); // A list containing the commands to be executed.

        private readonly List<Component> instances = new List<Component>(); // A list containing the instances of components that can invoke this command.

        private readonly ICommandManager? manager; // A reference to the command manager that manages this command and its invokers.

        private bool isChecked = false; // The checked state of the control instances.

        private bool isEnabled = true; // The enabled state of the control instanced.

        /// <summary>
        /// Initialises a new instance of the <see cref="AggregateCommand"/> class.
        /// </summary>
        /// <param name="manager">The <see cref="ICommandManager"/> that manages this command and its invokers.</param>
        /// <param name="commands">A collection containing the commands to be executed.</param>
        public AggregateCommand(ICommandManager manager, IEnumerable<ICommand> commands)
        {
            if (commands != null) this.commands.AddRange(commands);

            this.manager = manager;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AggregateCommand"/> class.
        /// </summary>
        /// <param name="commands">A collection containing the commands to be executed.</param>
        public AggregateCommand(IEnumerable<ICommand> commands)
        {
            if (commands != null) this.commands.AddRange(commands);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AggregateCommand"/> class.
        /// </summary>
        /// <param name="manager">The <see cref="ICommandManager"/> that manages this command and its invokers.</param>
        /// <param name="commands">The commands to be executed.</param>
        public AggregateCommand(ICommandManager manager, params ICommand[] commands)
        {
            if (commands != null) this.commands.AddRange(commands);

            this.manager = manager;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AggregateCommand"/> class.
        /// </summary>
        /// <param name="commands">The commands to be executed.</param>
        public AggregateCommand(params ICommand[] commands)
        {
            if (commands != null) this.commands.AddRange(commands);
        }

        #region ICommand Members

        /// <summary>
        /// Executes the commands sequentially.
        /// </summary>
        public virtual void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        #endregion

        #region IComponentCommand Members

        /// <summary>
        /// Gets or sets the Checked property of the controls that can execute this command.
        /// </summary>
        public bool Checked
        {
            get
            {
                return isChecked;
            }

            set
            {
                if (manager != null)
                {
                    foreach (var instance in instances)
                    {
                        manager.GetCommandInvoker(instance).UpdateCheckedState(instance, value);
                    }

                    isChecked = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Enabled property of the controls that can execute this command.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                if (manager != null)
                {
                    foreach (var instance in instances)
                    {
                        manager.GetCommandInvoker(instance).UpdateEnabledState(instance, value);
                    }

                    isEnabled = value;
                }
            }
        }

        /// <summary>
        /// Adds a component to the list of components that can execute this command.
        /// </summary>
        /// <param name="instance">The component to be added e.g. a ToolStripMenuItem.</param>
        public void AddInstance(Component instance)
        {
            if (manager != null)
            {
                instances.Add(instance);

                var invoker = manager.GetCommandInvoker(instance);

                invoker.AddInstance(instance, this);

                invoker.UpdateCheckedState(instance, Checked);
                invoker.UpdateEnabledState(instance, Enabled);
            }
        }

        #endregion
    }
}
