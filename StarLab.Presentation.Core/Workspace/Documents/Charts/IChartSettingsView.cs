namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChartSettingsViewPresenter"/> to control the behaviour of a chart settings panel.
    /// </summary>
    public interface IChartSettingsView : IChildView, ICancellableView
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
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        /// <param name="hideBackGround">A flag that controls whether the background colour options are displayed.</param>
        void AppendColourSection(string key, bool hideBackGround);

        /// <summary>
        /// Appends a data source settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        /// <param name="horizontalAxisFields">The fields that can be represented on a horizontal axis.</param>
        /// <param name="verticalAxisFields">The fields that can be represented on a vertical axis.</param>
        void AppendDataSourceSection(string key, IEnumerable<IDisplayField> horizontalAxisFields, IEnumerable<IDisplayField> verticalAxisFields);

        /// <summary>
        /// Appends a font settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        /// <param name="hideStyles">A flag that controls whether the font styles are displayed.</param>
        void AppendFontSection(string key, bool hideStyles);

        /// <summary>
        /// Appends a scale settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        void AppendScaleSection(string key);

        /// <summary>
        /// Appends a size settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        void AppendSizeSection(string key);

        /// <summary>
        /// Appends a text settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        void AppendTextSection(string key);

        /// <summary>
        /// Appends a visibility settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        void AppendVisibleSection(string key);

        /// <summary>
        /// Clears the settings panel.
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears the overlay settings.
        /// </summary>
        void ClearOverlays();

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

        /// <summary>
        /// Updates the chart settings following a change.
        /// </summary>
        /// <param name="chart">The new chart settings.</param>
        void UpdateSettings(IChartSettings chart);
    }
}
