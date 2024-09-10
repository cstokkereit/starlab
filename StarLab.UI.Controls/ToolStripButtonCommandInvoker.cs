using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// Invokes the command that is associated with a ToolStripButton.
    /// </summary>
    public class ToolStripButtonCommandInvoker : CommandInvoker<ToolStripButton>
    {
        /// <summary>
        /// Associates a command with the ToolStripButton that invokes it.
        /// </summary>
        /// <param name="item">The ToolStripButton that will invoke the command.</param>
        /// <param name="command">The command that will be invoked.</param>
        public override void AddInstance(Component component, ICommand command)
        {
            if (component is ToolStripButton control) control.Click += OnClick;
            base.AddInstance(component, command);
        }

        /// <summary>
        /// Dissociates a command from the ToolStripButton that invokes it.
        /// </summary>
        /// <param name="component">The ToolStripButton that will no longer invoke the command.</param>
        public override void RemoveInstance(Component component)
        {
            if (component is ToolStripButton control) control.Click -= OnClick;
            base.RemoveInstance(component);
        }

        /// <summary>
        /// Updates the Checked state of the ToolStripButton provided.
        /// </summary>
        /// <param name="component">The ToolStripButton being updated.</param>
        /// <param name="value">The new Checked state.</param>
        public override void UpdateCheckedState(Component component, bool value)
        {
            if (component is ToolStripButton control) control.Checked = value;
        }

        /// <summary>
        /// Updates the Enabled state of the ToolStripButton provided.
        /// </summary>
        /// <param name="component">The ToolStripButton being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public override void UpdateEnabledState(Component component, bool value)
        {
            if (component is ToolStripButton control) control.Enabled = value;
        }

        /// <summary>
        /// Handler for the ToolStripButton Click event.
        /// </summary>
        /// <param name="sender">The control that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnClick(object sender, EventArgs e)
        {
            var command = GetCommandForInstance((ToolStripButton)sender);
            if (command != null) command.Execute();
        }
    }
}
