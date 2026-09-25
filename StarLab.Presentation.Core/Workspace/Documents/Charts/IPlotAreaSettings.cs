namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the plot area while the chart is being configured.
    /// </summary>
    public interface IPlotAreaSettings
    {
        /// <summary>
        /// Gets or sets the background colour of the plot area.
        /// </summary>
        string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the foreground colour of the plot area.
        /// </summary>
        string ForeColour { get; set; }

        /// <summary>
        /// Gets the chart grid settings.
        /// </summary>
        IGridSettings Grid { get; }

        /// <summary>
        /// Gets the data point settings.
        /// </summary>
        IPointSettings Points { get; }

        /// <summary>
        /// Gets or sets a flag that determines whether the chart element is visible.
        /// </summary>
        bool Visible { get; set; }
    }
}
