namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart axis.
    /// </summary>
    public interface IAxis
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
        /// Gets the axis label.
        /// </summary>
        ILabel Label { get; }

        /// <summary>
        /// A flag indicating whether the axis is visible.
        /// </summary>
        bool Visible { get; }
    }
}
