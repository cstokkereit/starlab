namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart grid.
    /// </summary>
    public interface IGrid : IFrameElement
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
