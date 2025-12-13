namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the plot area while the chart is being configured.
    /// </summary>
    internal class PlotAreaSettings : IPlotAreaSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlotAreaSettings"/> class.
        /// </summary>
        /// <param name="plotArea">An <see cref="IPlotArea"/> that specifies the initial state of the plot area.</param>
        public PlotAreaSettings(IPlotArea plotArea)
        {
            BackColour = plotArea.BackColour;
            ForeColour = plotArea.ForeColour;

            Grid = new GridSettings(plotArea.Grid);
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour { get; set; }


        /// <summary>
        /// Gets the grid settings.
        /// </summary>
        public IGridSettings Grid { get; }
    }
}
