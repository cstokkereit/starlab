namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart grid.
    /// </summary>
    public interface IGrid
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
        /// Gets the major grid lines.
        /// </summary>
        IGridLines MajorGridLines { get; }

        /// <summary>
        /// Gets the minor grid lines.
        /// </summary>
        IGridLines MinorGridLines { get; }

        /// <summary>
        /// A flag indicating whether the grid is visible.
        /// </summary>
        bool Visible { get; }
    }
}
