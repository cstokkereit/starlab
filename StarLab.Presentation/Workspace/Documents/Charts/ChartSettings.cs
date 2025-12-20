namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart while it is being configured.
    /// </summary>
    internal class ChartSettings : ChartElementSettings, IChartSettings
    {
        private string backColour; // TODO

        private string foreColour; // TODO

        private IFont font; // TODO

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettings"> class.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the initial state of the chart.</param>
        public ChartSettings(IChart chart)
            : base(true)
        {
            Axes = new AxesSettings(chart.X1, chart.X2, chart.Y1, chart.Y2);
            PlotArea = new PlotAreaSettings(chart.PlotArea, true);
            Title = new LabelSettings(chart.Title);

            backColour = chart.BackColour;
            foreColour = chart.ForeColour;

            font = new Font(chart.Font);
        }

        /// <summary>
        /// Gets the chart axis settings.
        /// </summary>
        public IAxesSettings Axes { get; }

        /// <summary>
        /// Gets or sets the chart background colour.
        /// </summary>
        public string BackColour 
        {
            get
            {
                return backColour;
            }

            set
            {
                PlotArea.BackColour = value;

                backColour = value;
            }
        }

        /// <summary>
        /// Gets or sets the chart font.
        /// </summary>
        public IFont Font
        {
            get => font;

            set
            {
                Title.Font = value;
                Axes.Font = value;

                font = value;
            }
        }

        /// <summary>
        /// Gets or sets the chart foreground colour.
        /// </summary>
        public string ForeColour
        {
            get
            {
                return foreColour;
            }

            set
            {
                PlotArea.ForeColour = value;
                Title.Colour = value;
                Axes.Colour = value;

                foreColour = value;
            }
        }

        /// <summary>
        /// Gets the plot area settings.
        /// </summary>
        public IPlotAreaSettings PlotArea { get; }

        /// <summary>
        /// Gets the chart title.
        /// </summary>
        public ILabelSettings Title { get; }
    }
}
