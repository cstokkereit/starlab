using System.ComponentModel;

namespace Stratosoft.Commands
{
    /// <summary>
    /// Used to bind commands to the instances of the components that invoke them.
    /// </summary>
    public interface ICommandInvoker
    {
        /// <summary>
        /// Gets the type of the <see cref="Component"/> that invokes the commands e.g. System.Windows.Forms.Button
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Associates an <see cref="ICommand"/> with the <see cref="Component"/> that invokes it.
        /// </summary>
        /// <param name="item">The <see cref="Component"/> that will invoke the command.</param>
        /// <param name="command">The <see cref="ICommand"/> that will be invoked.</param>
        void AddInstance(Component component, ICommand command);

        /// <summary>
        /// Dissociates an <see cref="ICommand"/> from the <see cref="Component"/> that invokes it.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> that will no longer invoke the command.</param>
        void RemoveInstance(Component component);

        /// <summary>
        /// Updates the Checked state of the <see cref="Component"/> provided.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> being updated.</param>
        /// <param name="value">The new Checked state.</param>
        void UpdateCheckedState(Component component, bool value);

        /// <summary>
        /// Updates the Enabled state of the <see cref="Component"/> provided.
        /// </summary>
        /// <param name="component">The <see cref="Component"/> being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        void UpdateEnabledState(Component component, bool value);
    }
}
