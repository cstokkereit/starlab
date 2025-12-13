namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart grid lines.
    /// </summary>
    public interface IGridLines
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
        /// A flag indicating that the axis is visible.
        /// </summary>
        bool Visible { get; }
    }
}
