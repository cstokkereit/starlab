using StarLab.Commands.Properties;
using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// Abstract base class for creating strongly typed CommandInvoker classes that bind commands to the instances of the components that invoke them.
    /// Each class derived from <see cref="CommandInvoker&lt;TComponent&gt;"/> handles only one type of component e.g. a System.Windows.Forms.Button
    /// </summary>
    /// <typeparam name="TComponent">The type of the component that will invoke the commands.</typeparam>
    public abstract class CommandInvoker<TComponent> : ICommandInvoker
    {
        private Dictionary<Component, ICommand> commands = new Dictionary<Component, ICommand>(); // A dictionary containing the commands indexed by component instance.

        /// <summary>
        /// Gets the type of the component that invokes the commands e.g. System.Windows.Forms.Button
        /// </summary>
        public string Type { get { return typeof(TComponent).ToString(); } }

        /// <summary>
        /// Associates a command with the component that invokes it.
        /// </summary>
        /// <param name="item">The component that will invoke the command.</param>
        /// <param name="command">The command that will be invoked.</param>
        public virtual void AddInstance(Component component, ICommand command)
        {
            Validate(component);

            if (command != null && !commands.ContainsKey(component)) commands.Add(component, command);
        }

        /// <summary>
        /// Dissociates a command from the component that invokes it.
        /// </summary>
        /// <param name="component">The component that will no longer invoke the command.</param>
        public virtual void RemoveInstance(Component component)
        {
            if (component != null && commands.ContainsKey(component)) commands.Remove(component);
        }

        /// <summary>
        /// Updates the Checked state of the component instance provided when overridden in a derived class.
        /// </summary>
        /// <param name="component">The component being updated.</param>
        /// <param name="value">The new Checked state.</param>
        public virtual void UpdateCheckedState(Component component, bool value)
        {
            // Do Nothing - Not all components have a Checked state.
        }

        /// <summary>
        /// Updates the Enabled state of the component instance provided when overridden in a derived class.
        /// </summary>
        /// <param name="component">The component being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public virtual void UpdateEnabledState(Component component, bool value)
        {
            // Do Nothing - Not all components have an Enabled state.
        }

        /// <summary>
        /// Gets the command associated with the specified component instance.
        /// </summary>
        /// <param name="component">The component instance.</param>
        /// <returns>The required command.</returns>
        protected ICommand GetCommandForInstance(Component component)
        {
            return commands[component];
        }

        /// <summary>
        /// A helper function that casts the component provided to the component type specified by the TComponent type argument.
        /// </summary>
        /// <param name="component">The component instance that is to be cast to the specified type.</param>
        /// <returns>An instance of the type specified by TComponent.</returns>
        protected TComponent Cast(Component component)
        {
            object temp = component;
            return (TComponent)(temp);
        }

        /// <summary>
        /// Validates the component provided to ensure it is of the type specified by the TComponent type argument.
        /// </summary>
        /// <param name="component">The component to be validated.</param>
        private void Validate(Component component)
        {
            if (component == null) throw new ArgumentNullException("component");

            if (typeof(TComponent) != component.GetType()) throw new ArgumentException(string.Format(Resources.MessageInvalidComponentType, typeof(TComponent), component.GetType()));
        }
    }
}
