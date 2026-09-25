namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the plot area of a chart.
    /// </summary>
    public interface IPlotArea
    {
        /// <summary>
        /// Gets the background colour of the plot area.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour of the plot area.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the chart grid.
        /// </summary>
        IGrid Grid { get; }

        /// <summary>
        /// Gets the chart data points.
        /// </summary>
        IPoints Points { get; }

        /// <summary>
        /// A flag indicating that the plot area is visible.
        /// </summary>
        bool Visible { get; }
    }
}
