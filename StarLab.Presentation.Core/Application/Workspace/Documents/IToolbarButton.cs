using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents a toolbar button.
    /// </summary>
    public interface IToolbarButton
    {
        /// <summary>
        /// Gets the <see cref="ICommand"/> that will be executed when the button is clicked.
        /// </summary>
        ICommand Command { get; }

        /// <summary>
        /// Gets the <see cref="System.Drawing.Image"/> that represents the button.
        /// </summary>
        Image Image { get; }

        /// <summary>
        /// Gets the name of the button.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the tooltip text. 
        /// </summary>
        string Tooltip { get; }
    }
}
