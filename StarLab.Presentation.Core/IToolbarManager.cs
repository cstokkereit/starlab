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
        /// <param name="image">The <see cref="Image"> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"> to invoke when the button is clicked.</param>
        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);
    }
}
