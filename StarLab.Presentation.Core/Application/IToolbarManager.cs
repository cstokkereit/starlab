using StarLab.Commands;

namespace StarLab.Application
{
    public interface IToolbarManager
    {
        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The image to use for the button.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);
    }
}
