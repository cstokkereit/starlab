namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the data points while the chart is being configured.
    /// </summary>
    public interface IPointSettings : IChartElementSettings
    {
        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        string Colour { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        int Size { get; set; }
    }
}
