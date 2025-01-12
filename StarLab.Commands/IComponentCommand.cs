using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// Represents a command that is invoked by a user interface component e.g. System.Windows.Forms.Button.
    /// </summary>
    public interface IComponentCommand
    {
        /// <summary>
        /// Gets or sets the Checked property of the controls that can execute this command.
        /// </summary>
        bool Checked { get; set; }

        /// <summary>
        /// Gets or sets the Enabled property of the controls that can execute this command.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Adds the <see cref="Component"> provided to the list of components that can execute this command.
        /// </summary>
        /// <param name="instance">The <see cref="Component"> being added.</param>
        void AddInstance(Component instance);
    }
}
