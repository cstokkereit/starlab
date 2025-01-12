using StarLab.Commands;
using System.ComponentModel;

namespace StarLab.Application
{
    /// <summary>
    /// Invokes the <see cref="ICommand"> that is associated with a <see cref="Button"/>.
    /// </summary>
    public class ButtonCommandInvoker : CommandInvoker<Button>
    {
        /// <summary>
        /// Associates an <see cref="ICommand"> with the <see cref="Button"/> that invokes it.
        /// </summary>
        /// <param name="item">The <see cref="Button"/> that will invoke the <see cref="ICommand">.</param>
        /// <param name="command">The <see cref="ICommand"> that will be invoked.</param>
        public override void AddInstance(Component component, ICommand command)
        {
            if (component is Button control) control.Click += Button_Click;

            base.AddInstance(component, command);
        }

        /// <summary>
        /// Dissociates an <see cref="ICommand"> from the <see cref="Button"/> that invokes it.
        /// </summary>
        /// <param name="component">The <see cref="Button"/> that will no longer invoke the <see cref="ICommand">.</param>
        public override void RemoveInstance(Component component)
        {
            if (component is Button control) control.Click -= Button_Click;

            base.RemoveInstance(component);
        }

        /// <summary>
        /// Updates the Enabled state of the <see cref="Button"/> provided.
        /// </summary>
        /// <param name="component">The <see cref="Button"/> being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public override void UpdateEnabledState(Component component, bool value)
        {
            if (component is Button control) control.Enabled = value;
        }

        /// <summary>
        /// Event handler for the <see cref="Button.Click"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void Button_Click(object? sender, EventArgs? e)
        {
            if (sender != null)
            {
                var command = GetCommandForInstance((Button)sender);
                command?.Execute();
            }
        }
    }
}
