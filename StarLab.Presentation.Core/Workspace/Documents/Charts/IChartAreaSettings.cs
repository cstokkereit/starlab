namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a coloured area while the chart is being configured.
    /// </summary>
    public interface IChartAreaSettings : IChartElement
    {
        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        string ForeColour { get; set; }
    }
}
