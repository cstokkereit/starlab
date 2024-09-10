using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// Used to bind commands to the instances of the components that invoke them.
    /// </summary>
    public interface ICommandInvoker
    {
        /// <summary>
        /// Gets the type of the component that invokes the commands e.g. System.Windows.Forms.Button
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Associates a command with the component that invokes it.
        /// </summary>
        /// <param name="item">The component that will invoke the command.</param>
        /// <param name="command">The command that will be invoked.</param>
        void AddInstance(Component component, ICommand command);

        /// <summary>
        /// Dissociates a command from the component that invokes it.
        /// </summary>
        /// <param name="component">The component that will no longer invoke the command.</param>
        void RemoveInstance(Component component);

        /// <summary>
        /// Updates the Checked state of the component provided.
        /// </summary>
        /// <param name="component">The component being updated.</param>
        /// <param name="value">The new Checked state.</param>
        void UpdateCheckedState(Component component, bool value);

        /// <summary>
        /// Updates the Enabled state of the component provided.
        /// </summary>
        /// <param name="component">The component being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        void UpdateEnabledState(Component component, bool value);
    }
}
