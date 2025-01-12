using StarLab.Commands;

namespace StarLab.Application
{
    /// <summary>
    /// Extends the <see cref="System.Windows.Forms.ToolStrip"/> control.
    /// </summary>
    public class ToolStrip : System.Windows.Forms.ToolStrip
    {
        /// <summary>
        /// Adds a <see cref="ToolStripButton"> to the <see cref="System.Windows.Forms.ToolStrip"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ToolStripButton">.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"> that will be invoked when the button is clicked.</param>
        public void AddButton(string name, string tooltip, Image? image, ICommand command)
        {
            var button = new ToolStripButton(image);

            button.ToolTipText = tooltip;

            Items.Add(button);

            if (button != null && command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(button);
            }
        }
    }
}
