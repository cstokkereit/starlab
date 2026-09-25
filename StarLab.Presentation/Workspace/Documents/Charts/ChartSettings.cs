namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart while it is being configured.
    /// </summary>
    internal class ChartSettings : IChartSettings
    {
        private readonly Dictionary<string, IChartElementSettings> settingsByKey = new Dictionary<string, IChartElementSettings>(); // A dictionary that holds the settings for the chart elements indexed by key.

        private string backColour; // The chart background colour.

        private string foreColour; // The chart foreground colour.

        private IFont font; // The chart font.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettings"> class.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the initial state of the chart.</param>
        public ChartSettings(IChart chart)
        {
            Axes = new AxesSettings(chart.X1, chart.X2, chart.Y1, chart.Y2);
            PlotArea = new PlotAreaSettings(chart.PlotArea);
            Title = new LabelSettings(chart.Title);

            backColour = chart.BackColour;
            foreColour = chart.ForeColour;

            font = new Font(chart.Font);

            PopulateSettingsByKey();
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
                PlotArea.BackColour = value;
                Title.Colour = value;
                Axes.Colour = value;

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
            get => foreColour;

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

        /// <summary>
        /// Gets or sets a flag indicating that the chart element is visible.
        /// </summary>
        public bool Visible { get => true; set => throw new NotSupportedException(); }

        /// <summary>
        /// Gets the <see cref="IChartElementSettings"/> with the specified key.
        /// </summary>
        /// <param name="key">The chart element key.</param>
        /// <returns>The <see cref="IChartElementSettings"/> with the specified key.</returns>
        public IChartElementSettings GetSettings(string key)
        {
            return settingsByKey[key];
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="settings"></param>
        private void AddAxisSettings(string name, string parent, IAxisSettings settings)
        {
            var axis = AddSettings(name, parent, settings);

            AddSettings(Constants.Label, axis, settings.Label);

            var scale = AddSettings(Constants.Scale, axis, settings.Scale);

            AddSettings(Constants.MajorTickMarks, scale, settings.Scale.MajorTickMarks);
            AddSettings(Constants.MinorTickMarks, scale, settings.Scale.MinorTickMarks);
            AddSettings(Constants.TickLabels, scale, settings.Scale.TickLabels);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private string AddSettings(string name, string parent, IChartElementSettings settings)
        {
            var key = $"{parent}/{name}";

            settingsByKey.Add(key, settings);

            return key;
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void PopulateSettingsByKey()
        {
            // TODO - Fix XML mapping and save/load workspace
            // Switch from chart.Backcolour to dto.Backcolour now it has one
            // Get rid of settings groups and managers etc if no longer needed
            // Clean out unused path constants
            // Fix tests that have changed - lots
            // Debug and Tidy up

            AddSettings(Constants.Title, Constants.Chart, Title);

            var axes = AddSettings(Constants.Axes, Constants.Chart, Axes);

            AddAxisSettings(Constants.AxisX1, axes, Axes.X1);
            AddAxisSettings(Constants.AxisX2, axes, Axes.X2);
            AddAxisSettings(Constants.AxisY1, axes, Axes.Y1);
            AddAxisSettings(Constants.AxisY2, axes, Axes.Y2);

            var plotArea = $"{Constants.Chart}/{Constants.PlotArea}";

            AddSettings(Constants.Points, plotArea, PlotArea.Points);

            var grid = AddSettings(Constants.Grid, plotArea, PlotArea.Grid);

            AddSettings(Constants.MajorGridLines, grid, PlotArea.Grid.MajorGridLines);
            AddSettings(Constants.MinorGridLines, grid, PlotArea.Grid.MinorGridLines);
        }
    }
}
