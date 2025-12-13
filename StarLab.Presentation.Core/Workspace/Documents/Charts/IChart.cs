namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart.
    /// </summary>
    public interface IChart
    {
        /// <summary>
        /// Gets the background colour.
        /// </summary>
        string BackColour { get; }

        /// <summary>
        /// Gets the font.
        /// </summary>
        IFont Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        string ForeColour { get; }

        /// <summary>
        /// Gets the plot area.
        /// </summary>
        IPlotArea PlotArea { get; }

        /// <summary>
        /// Gets the chart title label.
        /// </summary>
        ILabel Title { get; }

        /// <summary>
        /// Gets the bottom axis.
        /// </summary>
        IAxis X1 { get; }

        /// <summary>
        /// Gets the top axis.
        /// </summary>
        IAxis X2 { get; }

        /// <summary>
        /// Gets the left axis.
        /// </summary>
        IAxis Y1 { get; }

        /// <summary>
        /// Gets the right axis.
        /// </summary>
        IAxis Y2 { get; }
    }
}
