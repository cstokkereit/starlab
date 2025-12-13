namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart while it is being configured.
    /// </summary>
    internal class ChartSettings : IChartSettings
    {
        private readonly IFontSettings fontSettings;

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettings"> class.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the initial state of the chart.</param>
        public ChartSettings(IChart chart)
        {
            Axes = new AxesSettings(chart.X1, chart.X2, chart.Y1, chart.Y2);
            PlotArea = new PlotAreaSettings(chart.PlotArea);
            Title = new LabelSettings(chart.Title);

            fontSettings = new FontSettings(chart.Font);
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
                return GetColour(x => x.BackColour);
            }

            set
            {
                Title.BackColour = value;
                Axes.BackColour = value;
            }
        }

        /// <summary>
        /// Gets or sets the chart font.
        /// </summary>
        public IFontSettings Font
        {
            get => GetFont();

            set
            {
                Title.Font.SetFont(value);
                Axes.Font.SetFont(value);
            }
        }

        /// <summary>
        /// Gets or sets the chart foreground colour.
        /// </summary>
        public string ForeColour
        {
            get
            {
                return GetColour(x => x.ForeColour);
            }

            set
            {
                Title.ForeColour = value;
                Axes.ForeColour = value;
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

        /// <summary>
        /// Gets the colour applied to the greatest number of chart elements.
        /// </summary>
        /// <param name="Colour">A function that determines which colour setting is used.</param>
        /// <returns>A <see cref="string"/> that specifies the colour applicable to the greatest number of axes.</returns>
        private string GetColour(Func<IColourSettings, string> Colour)
        {
            var colours = new List<string>();

            if (Title.Visible) colours.Add(Colour(Title));
            if (Axes.Visible) colours.Add(Colour(Axes));

            return colours.Count == 0 ? string.Empty : colours.GroupBy(a => a).OrderByDescending(b => b.Count()).First().Key;
        }

        /// <summary>
        /// Gets the font applied to the greatest number of chart elements.
        /// </summary>
        /// <returns>A <see cref="string"/> that specifies the font applicable to the greatest number of chart elements.</returns>
        private IFontSettings GetFont()
        {
            var fonts = new List<IFontSettings>();

            if (Title.Visible) fonts.Add(Title.Font);
            if (Axes.Visible) fonts.Add(Axes.Font);

            var orderedFonts = fonts.GroupBy(a => a.Family).OrderByDescending(b => b.Count());

            return fonts.Count == 0 ? fontSettings : fonts.GroupBy(a => a).OrderByDescending(b => b.Count()).First().Key;
        }
    }
}
