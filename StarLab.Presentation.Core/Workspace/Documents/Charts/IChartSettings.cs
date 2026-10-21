using StarLab.Presentation.Workspace.Documents.Charts.Overlays;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the chart while the it is being configured.
    /// </summary>
    public interface IChartSettings
    {
        /// <summary>
        /// Gets the chart axis settings.
        /// </summary>
        IAxesSettings Axes { get; }

        /// <summary>
        /// Gets or sets the chart background colour.
        /// </summary>
        public string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the chart font.
        /// </summary>
        IFont Font { get; set; }

        /// <summary>
        /// Gets or sets the chart foreground colour.
        /// </summary>
        public string ForeColour { get; set; }

        /// <summary>
        /// Gets the names of the chart overlays.
        /// </summary>
        IReadOnlyList<IOverlaySettings> Overlays { get; }

        /// <summary>
        /// Gets the plot area settings.
        /// </summary>
        IPlotAreaSettings PlotArea { get; }

        /// <summary>
        /// Gets the chart title.
        /// </summary>
        ILabelSettings Title { get; }

        /// <summary>
        /// Gets the specified <see cref="IOverlay"/>.
        /// </summary>
        /// <param name="name">The name of the overlay.</param>
        /// <returns>TODO</returns>
        IOverlaySettings GetOverlay(string name);

        /// <summary>
        /// Gets the <see cref="IChartElementSettings"/> with the specified key.
        /// </summary>
        /// <param name="key">The chart element key.</param>
        /// <returns>The <see cref="IChartElementSettings"/> with the specified key.</returns>
        IChartElementSettings GetSettings(string key);
    }
}
