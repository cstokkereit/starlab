namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart while it is being configured.
    /// </summary>
    internal class ChartSettings : IChartSettings
    {
        private IFontSettings font; // The current font settings.

        private string backColour; // The current background colour.

        private string foreColour; // The current foreground colour.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettings"> class.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the initial state of the chart.</param>
        public ChartSettings(IChart chart)
        {
            Axes = new AxesSettings(chart.X1, chart.X2, chart.Y1, chart.Y2);

            Title = new LabelSettings(chart.Title);
            backColour = chart.BackColour;
            foreColour = chart.ForeColour;
            
            font = new FontSettings(chart.Font);
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
            get => backColour;
            
            set
            {
                Title.BackColour = value;
                Axes.BackColour = value;
                backColour = value;
            }
        }

        /// <summary>
        /// Gets or sets the chart font.
        /// </summary>
        public IFontSettings Font 
        {
            get => font;

            set
            {
                Title.Font.SetFont(value);
                Axes.Font.SetFont(value);
                font = value;
            }
        }

        /// <summary>
        /// Gets or sets the chart foreground colour.
        /// </summary>
        public string ForeColour
        {
            get => foreColour;

            set
            {
                Title.ForeColour = value;
                Axes.ForeColour = value;
                foreColour = value;
            }
        }

        /// <summary>
        /// Gets the chart title.
        /// </summary>
        public ILabelSettings Title { get; }
    }
}
