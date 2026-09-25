namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart grid.
    /// </summary>
    public interface IGrid : IChartElement
    {
        /// <summary>
        /// Gets the major grid lines.
        /// </summary>
        IGridLines MajorGridLines { get; }

        /// <summary>
        /// Gets the minor grid lines.
        /// </summary>
        IGridLines MinorGridLines { get; }
    }
}
