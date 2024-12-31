using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChartSettingsViewPresenter"/> to control the behaviour of a chart settings panel.
    /// </summary>
    public interface IChartSettingsView : IChildView
    {
        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the Cancel button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the Cancel button is clicked.</param>
        void AttachCancelButtonCommand(ICommand command);

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the OK button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the OK button is clicked.</param>
        void AttachOKButtonCommand(ICommand command);
    }
}
