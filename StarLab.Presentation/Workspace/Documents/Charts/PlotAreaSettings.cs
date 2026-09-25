namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the plot area while the chart is being configured.
    /// </summary>
    internal class PlotAreaSettings : IPlotAreaSettings
    {
        private string foreColour;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlotAreaSettings"/> class.
        /// </summary>
        /// <param name="plotArea">An <see cref="IPlotArea"/> that specifies the initial state of the plot area.</param>
        public PlotAreaSettings(IPlotArea plotArea)
        {
            Points = new PointSettings(plotArea.Points);

            Grid = new GridSettings(plotArea.Grid);

            BackColour = plotArea.BackColour;

            foreColour = plotArea.ForeColour;

            Visible = plotArea.Visible;
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
                Points?.Colour = value;
                Grid?.Colour = value;

                foreColour = value;
            }
        }

        /// <summary>
        /// Gets the grid settings.
        /// </summary>
        public IGridSettings Grid { get; }

        /// <summary>
        /// Gets the data point settings.
        /// </summary>
        public IPointSettings Points { get; }

        /// <summary>
        /// A flag indicating that the plot area is visible.
        /// </summary>
        public bool Visible { get; set; }
    }
}
