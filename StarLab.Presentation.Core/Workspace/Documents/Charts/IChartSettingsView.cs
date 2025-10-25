using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChartSettingsViewPresenter"/> to control the behaviour of a chart settings panel.
    /// </summary>
    public interface IChartSettingsView : IChildView
    {
        /// <summary>
        /// Adds a node to the tree view that displays the chart property groups.
        /// </summary>
        /// <param name="name">The node name.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <returns>The path to the new node.</returns>
        string AddNode(string name, string parentKey, string text);

        /// <summary>
        /// Adds a node to the tree view that displays the chart property groups.
        /// </summary>
        /// <param name="name">The node name.</param>
        /// <param name="text">The node text.</param>
        /// <returns>The path to the new node.</returns>
        string AddNode(string name, string text);

        /// <summary>
        /// Appends a colour settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group"></param>
        void AppendColourSection(IChartSettings settings, string group);

        /// <summary>
        /// Appends a font settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        void AppendFontSection(IChartSettings settings, string group);

        /// <summary>
        /// Appends a scale settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        void AppendScaleSection(IChartSettings settings, string group);

        /// <summary>
        /// Appends a text settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        void AppendTextSection(IChartSettings settings, string group);

        /// <summary>
        /// Appends a visibility settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        void AppendVisibleSection(IChartSettings settings, string group);

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

        /// <summary>
        /// Clears the settings panel.
        /// </summary>
        void Clear();

        /// <summary>
        /// Selects the specified tree view node.
        /// </summary>
        /// <param name="key">The node key.</param>
        void SelectNode(string key);
    }
}
