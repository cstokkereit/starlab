namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the chart while the it is being configured.
    /// </summary>
    public interface IChartSettings : IColourSettings
    {
        /// <summary>
        /// Gets the chart axis settings.
        /// </summary>
        IAxesSettings Axes { get; }

        /// <summary>
        /// Gets or sets the chart font.
        /// </summary>
        IFontSettings Font { get; set; }

        /// <summary>
        /// Gets the plot area settings.
        /// </summary>
        IPlotAreaSettings PlotArea { get; }

        /// <summary>
        /// Gets the chart title.
        /// </summary>
        ILabelSettings Title { get; }
    }
}
