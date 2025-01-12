using StarLab.Commands;
using System.ComponentModel;

namespace StarLab.Application
{
    /// <summary>
    /// Invokes the <see cref="ICommand"> that is associated with a <see cref="ToolStripMenuItem"/>.
    /// </summary>
    public class ToolStripMenuItemCommandInvoker : CommandInvoker<ToolStripMenuItem>
    {
        /// <summary>
        /// Associates an <see cref="ICommand"> with the <see cref="ToolStripMenuItem"/> that invokes it.
        /// </summary>
        /// <param name="item">The <see cref="ToolStripMenuItem"/> that will invoke the <see cref="ICommand">.</param>
        /// <param name="command">The <see cref="ICommand"> that will be invoked.</param>
        public override void AddInstance(Component component, ICommand command)
        {
            if (component is ToolStripMenuItem control) control.Click += ToolStripItem_Click;

            base.AddInstance(component, command);
        }

        /// <summary>
        /// Dissociates an <see cref="ICommand"> from the <see cref="ToolStripMenuItem"/> that invokes it.
        /// </summary>
        /// <param name="component">The <see cref="ToolStripMenuItem"/> that will no longer invoke the <see cref="ICommand">.</param>
        public override void RemoveInstance(Component component)
        {
            if (component is ToolStripMenuItem control) control.Click -= ToolStripItem_Click;

            base.RemoveInstance(component);
        }

        /// <summary>
        /// Updates the Checked state of the <see cref="ToolStripMenuItem"/> provided.
        /// </summary>
        /// <param name="component">The <see cref="ToolStripMenuItem"/> being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public override void UpdateCheckedState(Component component, bool value)
        {
            if (component is ToolStripMenuItem control) control.Checked = value;
        }

        /// <summary>
        /// Updates the Enabled state of the <see cref="ToolStripMenuItem"/> provided.
        /// </summary>
        /// <param name="component">The <see cref="ToolStripMenuItem"/> being updated.</param>
        /// <param name="value">The new Enabled state.</param>
        public override void UpdateEnabledState(Component component, bool value)
        {
            if (component is ToolStripMenuItem control) control.Enabled = value;
        }

        /// <summary>
        /// Event handler for the <see cref="ToolStripItem.Click"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void ToolStripItem_Click(object? sender, EventArgs? e)
        {
            if (sender != null)
            {
                var command = GetCommandForInstance((ToolStripMenuItem)sender);
                command?.Execute();
            }
        }
    }
}
