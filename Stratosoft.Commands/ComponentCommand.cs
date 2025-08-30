using System.ComponentModel;

namespace Stratosoft.Commands
{
    /// <summary>
    /// Abstract base class for commands that are invoked by a user interface component e.g. System.Windows.Forms.Button and act on the specified type of receiver.
    /// </summary>
    /// <typeparam name="TReceiver">The type of receiver that this command acts on.</typeparam>
    public abstract class ComponentCommand<TReceiver> : Command<TReceiver>, ICommand, IComponentCommand
    {
        private readonly List<Component> instances = new List<Component>(); // A list containing the instances of components that can invoke this command.

        private readonly ICommandManager manager; // A reference to the command manager that manages this command and its invokers.

        private bool isChecked = false; // The checked state of the control instances.

        private bool isEnabled = true; // The enabled state of the control instanced.

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentCommand{TReceiver}"/> class.
        /// </summary>
        /// <param name="manager">The <see cref="ICommandManager"/> that manages this command and its invokers.</param>
        /// <param name="receiver">The receiver that this command will act on.</param>
        public ComponentCommand(ICommandManager manager, TReceiver receiver)
            : base(receiver)
        {
            this.manager = manager ?? throw new ArgumentNullException("manager");
        }

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
                foreach (var instance in instances)
                {
                    manager.GetCommandInvoker(instance).UpdateCheckedState(instance, value);
                }

                isChecked = value;
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
                foreach (var instance in instances)
                {
                    manager.GetCommandInvoker(instance).UpdateEnabledState(instance, value);
                }

                isEnabled = value;
            }
        }

        /// <summary>
        /// Adds the <see cref="Component"> provided to the list of components that can execute this command.
        /// </summary>
        /// <param name="instance">The <see cref="Component"> being added.</param>
        public void AddInstance(Component instance)
        {
            instances.Add(instance);

            var invoker = manager.GetCommandInvoker(instance);

            invoker.AddInstance(instance, this);

            invoker.UpdateCheckedState(instance, Checked);
            invoker.UpdateEnabledState(instance, Enabled);
        }
    }
}
