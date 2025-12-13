namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart plot area.
    /// </summary>
    public interface IPlotArea
    {
        /// <summary>
        /// Gets the background colour.
        /// </summary>
        string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        string ForeColour { get; }

        /// <summary>
        /// Gets the chart grid.
        /// </summary>
        IGrid Grid { get; }
    }
}
