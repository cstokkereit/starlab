using Stratosoft.Commands.Properties;
using System.ComponentModel;

namespace Stratosoft.Commands
{
    /// <summary>
    /// Abstract base class for creating strongly typed CommandInvoker classes that bind commands to the instances of the components that invoke them.
    /// Each class derived from this abstract class handles one type of component e.g. a System.Windows.Forms.Button
    /// </summary>
    /// <typeparam name="TComponent">The type of the component that will invoke the commands.</typeparam>
    public abstract class CommandInvoker<TComponent> : ICommandInvoker
    {
        private Dictionary<Component, ICommand> commands = new Dictionary<Component, ICommand>(); // A dictionary containing the commands indexed by component instance.

        /// <summary>
        /// Gets the type of the <see cref="Component"/> that invokes the commands e.g. System.Windows.Forms.Button
        /// </summary>
        public string Type { get { return typeof(TComponent).ToString(); } }

        /// <summary>
        /// Associates an <see cref="ICommand"/> with the <see cref="Component"/> that invokes it.
        /// </summary>
        /// <param name="item">The <see cref="Component"/> that will invoke the command.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked.</param>
        public virtual void AddInstance(Component component, ICommand command)
        {
            Validate(component);

            if (command != null && !commands.ContainsKey(component)) commands.Add(component, command);
        }

        /// <summary>
        /// Dissociates an <see cref="ICommand"/> from the <see cref="Component"/> that invokes it.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> that will no longer invoke the command.</param>
        public virtual void RemoveInstance(Component component)
        {
            if (component != null && commands.ContainsKey(component)) commands.Remove(component);
        }

        /// <summary>
        /// Updates the Checked state of the <see cref="Component"/> provided.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> being updated.</param>
        /// <param name="value">The new Checked state.</param>
        public virtual void UpdateCheckedState(Component component, bool value)
        {
            // Do Nothing - Not all components have a Checked state.
        }

        /// <summary>
        /// Updates the Enabled state of the <see cref="Component"/> provided.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public virtual void UpdateEnabledState(Component component, bool value)
        {
            // Do Nothing - Not all components have an Enabled state.
        }

        /// <summary>
        /// Gets the <see cref="ICommand"/> associated with the specified <see cref="Component"/> instance.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> instance.</param>
        /// <returns>The required <see cref="ICommand"/>.</returns>
        protected ICommand GetCommandForInstance(Component component)
        {
            return commands[component];
        }

        /// <summary>
        /// A helper function that casts the <see cref="Component"/> provided to the component type specified by the <see cref="TComponent"/> type argument.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> instance that is to be cast to the specified type.</param>
        /// <returns>An instance of the type specified by <see cref="TComponent"/>.</returns>
        protected TComponent Cast(Component component)
        {
            object temp = component;
            return (TComponent)(temp);
        }

        /// <summary>
        /// Validates the component provided to ensure it is of the type specified by the <see cref="TComponent"/> type argument.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> being validated.</param>
        private void Validate(Component component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            if (typeof(TComponent) != component.GetType()) throw new ArgumentException(string.Format(Resources.MessageInvalidComponentType, typeof(TComponent), component.GetType()));
        }
    }
}
