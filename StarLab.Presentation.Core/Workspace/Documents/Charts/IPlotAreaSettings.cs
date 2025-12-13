namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the plot area while the chart is being configured.
    /// </summary>
    public interface IPlotAreaSettings : IColourSettings
    {
        /// <summary>
        /// Gets the chart grid settings.
        /// </summary>
        IGridSettings Grid { get; }
    }
}
