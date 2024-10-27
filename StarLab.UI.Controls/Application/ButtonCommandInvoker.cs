using StarLab.Commands;
using System.ComponentModel;

namespace StarLab.Application
{
    /// <summary>
    /// Invokes the command that is associated with a Button.
    /// </summary>
    public class ButtonCommandInvoker : CommandInvoker<Button>
    {
        /// <summary>
        /// Associates a command with the Button that invokes it.
        /// </summary>
        /// <param name="item">The Button that will invoke the command.</param>
        /// <param name="command">The command that will be invoked.</param>
        public override void AddInstance(Component component, ICommand command)
        {
            if (component is Button control) control.Click += OnClick;
            base.AddInstance(component, command);
        }

        /// <summary>
        /// Dissociates a command from the Button that invokes it.
        /// </summary>
        /// <param name="component">The Button that will no longer invoke the command.</param>
        public override void RemoveInstance(Component component)
        {
            if (component is Button control) control.Click -= OnClick;
            base.RemoveInstance(component);
        }

        /// <summary>
        /// Updates the Enabled state of the Button provided.
        /// </summary>
        /// <param name="component">The Button being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public override void UpdateEnabledState(Component component, bool value)
        {
            if (component is Button control) control.Enabled = value;
        }

        /// <summary>
        /// Handler for the Button Click event.
        /// </summary>
        /// <param name="sender">The control that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnClick(object? sender, EventArgs? e)
        {
            if (sender != null)
            {
                var command = GetCommandForInstance((Button)sender);
                command?.Execute();
            }
        }
    }
}
