using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a manager that can configure the behaviour of a toolbar.
    /// </summary>
    public interface IToolbarManager
    {
        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The button image.</param>
        /// <param name="command">The command to invoke when the button is clicked.</param>
        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);
    }
}
