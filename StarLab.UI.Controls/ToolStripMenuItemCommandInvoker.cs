using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// Invokes the command that is associated with a ToolStripMenuItem.
    /// </summary>
    public class ToolStripMenuItemCommandInvoker : CommandInvoker<ToolStripMenuItem>
    {
        /// <summary>
        /// Associates a command with the ToolStripMenuItem that invokes it.
        /// </summary>
        /// <param name="item">The ToolStripMenuItem that will invoke the command.</param>
        /// <param name="command">The command that will be invoked.</param>
        public override void AddInstance(Component component, ICommand command)
        {
            if (component is ToolStripMenuItem control) control.Click += OnClick;
            base.AddInstance(component, command);
        }

        /// <summary>
        /// Dissociates a command from the ToolStripMenuItem that invokes it.
        /// </summary>
        /// <param name="component">The ToolStripMenuItem that will no longer invoke the command.</param>
        public override void RemoveInstance(Component component)
        {
            if (component is ToolStripMenuItem control) control.Click -= OnClick;
            base.RemoveInstance(component);
        }

        /// <summary>
        /// Updates the Checked state of the ToolStripMenuItem provided.
        /// </summary>
        /// <param name="component">The ToolStripMenuItem being updated.</param>
        /// <param name="value">The new Checked state.</param>
        public override void UpdateCheckedState(Component component, bool value)
        {
            if (component is ToolStripMenuItem control) control.Checked = value;
        }

        /// <summary>
        /// Updates the Enabled state of the ToolStripMenuItem provided.
        /// </summary>
        /// <param name="component">The ToolStripMenuItem being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public override void UpdateEnabledState(Component component, bool value)
        {
            if (component is ToolStripMenuItem control) control.Enabled = value;
        }

        /// <summary>
        /// Handler for the ToolStripMenuItem Click event.
        /// </summary>
        /// <param name="sender">The control that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnClick(object sender, EventArgs e)
        {
            var command = GetCommandForInstance((ToolStripMenuItem)sender);
            if (command != null) command.Execute();
        }
    }
}
