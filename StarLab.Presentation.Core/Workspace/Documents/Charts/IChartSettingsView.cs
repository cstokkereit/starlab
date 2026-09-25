namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChartSettingsViewPresenter"/> to control the behaviour of a chart settings panel.
    /// </summary>
    public interface IChartSettingsView : IChildView, ISettingsView
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
        /// <param name="settings">An <see cref="IChartElementSettings"/> that represents the current state of the chart element.</param>
        void AppendColourSection(IChartElementSettings settings);

        /// <summary>
        /// Appends a colour settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        void AppendColourSection(IChartSettings settings);

        /// <summary>
        /// Appends a colour settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IPlotAreaSettings"/> that represents the current state of the plot area.</param>
        void AppendColourSection(IPlotAreaSettings settings);

        /// <summary>
        /// Appends a font settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IFontSettings"/> that TODO.</param>
        void AppendFontSection(IFontSettings settings);

        /// <summary>
        /// Appends a scale settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IScaleSettings"/> that represents the current state of the axis scale.</param>
        void AppendScaleSection(IScaleSettings settings);

        /// <summary>
        /// Appends a size settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IPointSettings"/> that represents the current state of the data points.</param>
        void AppendSizeSection(IPointSettings settings);

        /// <summary>
        /// Appends a text settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="ILabelSettings"/> that represents the current state of the label.</param>
        void AppendTextSection(ILabelSettings settings);

        /// <summary>
        /// Appends a visibility settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartElementSettings"/> that represents the current state of the chart element.</param>
        void AppendVisibleSection(IChartElementSettings settings);

        /// <summary>
        /// Clears the settings panel.
        /// </summary>
        void Clear();

        /// <summary>
        /// Expands the specified tree view node.
        /// </summary>
        /// <param name="key">The node key.</param>
        void ExpandNode(string key);

        /// <summary>
        /// Selects the specified tree view node.
        /// </summary>
        /// <param name="key">The node key.</param>
        void SelectNode(string key);
    }
}
