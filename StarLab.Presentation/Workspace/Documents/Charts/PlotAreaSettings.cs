namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the plot area while the chart is being configured.
    /// </summary>
    internal class PlotAreaSettings : ChartElementSettings, IPlotAreaSettings
    {
        private string foreColour; // The foreground colour.

        /// <summary>
        /// Initialises a new instance of the <see cref="PlotAreaSettings"/> class.
        /// </summary>
        /// <param name="plotArea">An <see cref="IPlotArea"/> that specifies the initial state of the plot area.</param>
        /// <param name="visible"></param>
        public PlotAreaSettings(IPlotArea plotArea, bool visible)
            : base(visible)
        {
            Grid = new GridSettings(plotArea.Grid);

            BackColour = plotArea.BackColour;
            
            foreColour = plotArea.ForeColour;
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour
        {
            get => foreColour;

            set
            {
                Grid.Colour = value;

                foreColour = value;
            }
        }

        /// <summary>
        /// Gets the grid settings.
        /// </summary>
        public IGridSettings Grid { get; }
    }
}
