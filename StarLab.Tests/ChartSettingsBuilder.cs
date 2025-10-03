using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.Tests
{
    /// <summary>
    /// A helper class that uses the Builder Pattern to mock the <see cref="IChartSettings"/> used for unit testing.
    /// </summary>
    public class ChartSettingsBuilder
    {
        private const string BLACK = "Black";
        private const string WHITE = "White";

        private IAxesSettings axes; // Mock axes settings.

        private IAxisSettings axisX1; // Mock axis settings.

        private IAxisSettings axisX2; // Mock axis settings.

        private IAxisSettings axisY1; // Mock axis settings.

        private IAxisSettings axisY2; // Mock axis settings.

        private IChartSettings chart; // Mock chart settings.

        private IFontSettings font; // Mock font settings.

        private ILabelSettings labelX1; // Mock label settings.

        private ILabelSettings labelX2; // Mock label settings.

        private ILabelSettings labelY1; // Mock label settings.

        private ILabelSettings labelY2; // Mock label settings.

        private ILabelSettings title; // Mock label settings.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsBuilder"/> class.
        /// </summary>
        /// <param name="backColour">The background colour.</param>
        /// <param name="foreColour">The foreground colour.</param>
        public ChartSettingsBuilder(string backColour, string foreColour)
        {
            font = Substitute.For<IFontSettings>();
            font.Family.Returns("Arial");
            font.Underline.Returns(false);
            font.Italic.Returns(false);
            font.Bold.Returns(false);
            font.Size.Returns(10);
            
            labelX1 = Substitute.For<ILabelSettings>();
            labelX1.BackColour.Returns(backColour);
            labelX1.ForeColour.Returns(foreColour);
            labelX1.Font.Returns(font);

            axisX1 = Substitute.For<IAxisSettings>();
            axisX1.Label.Returns(labelX1);
            axisX1.Visible.Returns(false);
            
            labelX2 = Substitute.For<ILabelSettings>();
            labelX2.BackColour.Returns(backColour);
            labelX2.ForeColour.Returns(foreColour);
            labelX2.Font.Returns(font);

            axisX2 = Substitute.For<IAxisSettings>();
            axisX2.Label.Returns(labelX2);
            axisX2.Visible.Returns(false);

            labelY1 = Substitute.For<ILabelSettings>();
            labelY1.BackColour.Returns(backColour);
            labelY1.ForeColour.Returns(foreColour);
            labelY1.Font.Returns(font);

            axisY1 = Substitute.For<IAxisSettings>();
            axisY1.Label.Returns(labelY1);
            axisY1.Visible.Returns(false);

            labelY2 = Substitute.For<ILabelSettings>();
            labelY2.BackColour.Returns(backColour);
            labelY2.ForeColour.Returns(foreColour);
            labelY2.Font.Returns(font);

            axisY2 = Substitute.For<IAxisSettings>();
            axisY2.Label.Returns(labelY2);
            axisY2.Visible.Returns(false);
            
            axes = Substitute.For<IAxesSettings>();
            axes.BackColour.Returns(backColour);
            axes.ForeColour.Returns(foreColour);
            axes.Font.Returns(font);
            axes.X1.Returns(axisX1);
            axes.X2.Returns(axisX2);
            axes.Y1.Returns(axisY1);
            axes.Y2.Returns(axisY2);
            
            title = Substitute.For<ILabelSettings>();
            title.BackColour.Returns(backColour);
            title.ForeColour.Returns(foreColour);
            title.Visible.Returns(false);
            title.Font.Returns(font);

            chart = Substitute.For<IChartSettings>();
            chart.BackColour.Returns(backColour);
            chart.ForeColour.Returns(foreColour);
            chart.Title.Returns(title);
            chart.Axes.Returns(axes);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsBuilder"/> class.
        /// </summary>
        public ChartSettingsBuilder()
            : this(WHITE, BLACK) { }

        /// <summary>
        /// Adds a primary x-axis to the chart settings.
        /// </summary>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="IChartSettings"/>.</returns>
        public ChartSettingsBuilder AddX1()
        {
            axisX1.Visible.Returns(true);
            return this;
        }

        /// <summary>
        /// Adds a secondary x-axis to the chart settings.
        /// </summary>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="IChartSettings"/>.</returns>
        public ChartSettingsBuilder AddX2()
        {
            axisX2.Visible.Returns(true);
            return this;
        }

        /// <summary>
        /// Adds a primary y-axis to the chart settings.
        /// </summary>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="IChartSettings"/>.</returns>
        public ChartSettingsBuilder AddY1()
        {
            axisY1.Visible.Returns(true);
            return this;
        }

        /// <summary>
        /// Adds a secondary y-axis to the chart settings.
        /// </summary>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="IChartSettings"/>.</returns>
        public ChartSettingsBuilder AddY2()
        {
            axisY2.Visible.Returns(true);
            return this;
        }

        /// <summary>
        /// Adds a title to the chart settings.
        /// </summary>
        /// <param name="text">The chart title text.</param>
        /// <param name="backColour">The chart title background colour.</param>
        /// <param name="foreColour">The chart title foreground colour.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="IChartSettings"/>.</returns>
        public ChartSettingsBuilder AddTitle(string text, string backColour, string foreColour)
        {
            title.BackColour.Returns(backColour);
            title.ForeColour.Returns(foreColour);
            title.Text.Returns(text);

            title.Visible.Returns(true);

            return this;
        }

        /// <summary>
        /// Adds a title to the chart settings.
        /// </summary>
        /// <param name="text">The chart title text.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="IChartSettings"/>.</returns>
        public ChartSettingsBuilder AddTitle(string text)
        {
            title.Text.Returns(text);

            return this;
        }

        /// <summary>
        /// Creates the <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="IChartSettings"/>.</returns>
        public IChartSettings CreateSettings()
        {
            return chart;
        }
    }
}
