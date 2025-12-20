namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart element while the chart is being configured.
    /// </summary>
    public interface IFrameElementSettings : IChartElementSettings
    {
        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        string Colour { get; set; }
    }
}
