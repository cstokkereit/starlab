using StarLab.Commands;

namespace StarLab.Application
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ToolStrip : System.Windows.Forms.ToolStrip
    {
        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The image to use for the button.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
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
