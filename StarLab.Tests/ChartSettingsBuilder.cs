using StarLab.Presentation.Workspace.Documents;
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

        private IFont font; // Mock font.

        private IGridSettings grid; // Mock grid settings.

        private ILabelSettings labelX1; // Mock label settings.

        private ILabelSettings labelX2; // Mock label settings.

        private ILabelSettings labelY1; // Mock label settings.

        private ILabelSettings labelY2; // Mock label settings.

        private IGridLineSettings majorGridLines; // Mock grid line settings.

        private ITickMarkSettings majorTickMarksX1; // Mock tick mark settings.

        private ITickMarkSettings majorTickMarksX2; // Mock tick mark settings.

        private ITickMarkSettings majorTickMarksY1; // Mock tick mark settings.

        private ITickMarkSettings majorTickMarksY2; // Mock tick mark settings.

        private IGridLineSettings minorGridLines; // Mock grid line settings.

        private ITickMarkSettings minorTickMarksX1; // Mock tick mark settings.

        private ITickMarkSettings minorTickMarksX2; // Mock tick mark settings.

        private ITickMarkSettings minorTickMarksY1; // Mock tick mark settings.

        private ITickMarkSettings minorTickMarksY2; // Mock tick mark settings.

        private IPlotAreaSettings plotArea; // Mock plot area settings.

        private IScaleSettings scaleX1; // Mock scale settings.

        private IScaleSettings scaleX2; // Mock scale settings.

        private IScaleSettings scaleY1; // Mock scale settings.

        private IScaleSettings scaleY2; // Mock scale settings.

        private ITickLabelSettings tickLabelsX1; // Mock tick label settings.

        private ITickLabelSettings tickLabelsX2; // Mock tick label settings.

        private ITickLabelSettings tickLabelsY1; // Mock tick label settings.

        private ITickLabelSettings tickLabelsY2; // Mock tick label settings.

        private ILabelSettings title; // Mock lable settings.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsBuilder"/> class.
        /// </summary>
        /// <param name="backColour">The background colour.</param>
        /// <param name="foreColour">The foreground colour.</param>
        public ChartSettingsBuilder(string backColour, string foreColour)
        {
            font = Substitute.For<IFont>();
            font.Family.Returns("Segoe UI");
            font.Underline.Returns(false);
            font.Italic.Returns(false);
            font.Bold.Returns(false);
            font.Size.Returns(10);
            
            labelX1 = Substitute.For<ILabelSettings>();
            labelX1.Colour.Returns(foreColour);
            labelX1.Font.Returns(font);

            majorGridLines = Substitute.For<IGridLineSettings>();
            majorGridLines.Colour.Returns(foreColour);
            majorGridLines.Visible.Returns(true);

            majorTickMarksX1 = Substitute.For<ITickMarkSettings>();
            majorTickMarksX1.Colour.Returns(foreColour);
            majorTickMarksX1.Length.Returns(4);
            majorTickMarksX1.Visible.Returns(true);

            minorGridLines = Substitute.For<IGridLineSettings>();
            minorGridLines.Colour.Returns(foreColour);
            minorGridLines.Visible.Returns(true);

            minorTickMarksX1 = Substitute.For<ITickMarkSettings>();
            minorTickMarksX1.Colour.Returns(foreColour);
            minorTickMarksX1.Length.Returns(2);
            minorTickMarksX1.Visible.Returns(true);

            tickLabelsX1 = Substitute.For<ITickLabelSettings>();
            tickLabelsX1.Colour.Returns(foreColour);
            tickLabelsX1.Font.Returns(font);
            tickLabelsX1.Visible.Returns(true);

            scaleX1 = Substitute.For<IScaleSettings>();
            scaleX1.MajorTickMarks.Returns(majorTickMarksX1);
            scaleX1.MajorTickMarks.Returns(minorTickMarksX1);
            scaleX1.TickLabels.Returns(tickLabelsX1);
            scaleX1.Colour.Returns(foreColour);
            scaleX1.Autoscale.Returns(false);
            scaleX1.Reversed.Returns(false);
            scaleX1.Visible.Returns(true);
            scaleX1.Maximum.Returns(10);
            scaleX1.Minimum.Returns(0);

            axisX1 = Substitute.For<IAxisSettings>();
            axisX1.Label.Returns(labelX1);
            axisX1.Scale.Returns(scaleX1);
            axisX1.Visible.Returns(false);

            labelX2 = Substitute.For<ILabelSettings>();
            labelX2.Colour.Returns(foreColour);
            labelX2.Font.Returns(font);

            majorTickMarksX2 = Substitute.For<ITickMarkSettings>();
            majorTickMarksX2.Colour.Returns(foreColour);
            majorTickMarksX2.Length.Returns(4);
            majorTickMarksX2.Visible.Returns(true);

            minorTickMarksX2 = Substitute.For<ITickMarkSettings>();
            minorTickMarksX2.Colour.Returns(foreColour);
            minorTickMarksX2.Length.Returns(2);
            minorTickMarksX2.Visible.Returns(true);

            tickLabelsX2 = Substitute.For<ITickLabelSettings>();
            tickLabelsX2.Colour.Returns(foreColour);
            tickLabelsX2.Font.Returns(font);
            tickLabelsX2.Visible.Returns(true);

            scaleX2 = Substitute.For<IScaleSettings>();
            scaleX2.MajorTickMarks.Returns(majorTickMarksX2);
            scaleX2.MajorTickMarks.Returns(minorTickMarksX2);
            scaleX2.TickLabels.Returns(tickLabelsX2);
            scaleX2.Colour.Returns(foreColour);
            scaleX2.Autoscale.Returns(false);
            scaleX2.Reversed.Returns(false);
            scaleX2.Visible.Returns(true);
            scaleX2.Maximum.Returns(10);
            scaleX2.Minimum.Returns(0);

            axisX2 = Substitute.For<IAxisSettings>();
            axisX2.Label.Returns(labelX2);
            axisX2.Scale.Returns(scaleX2);
            axisX2.Visible.Returns(false);

            labelY1 = Substitute.For<ILabelSettings>();
            labelY1.Colour.Returns(foreColour);
            labelY1.Font.Returns(font);

            majorTickMarksY1 = Substitute.For<ITickMarkSettings>();
            majorTickMarksY1.Colour.Returns(foreColour);
            majorTickMarksY1.Length.Returns(4);
            majorTickMarksY1.Visible.Returns(true);

            minorTickMarksY1 = Substitute.For<ITickMarkSettings>();
            minorTickMarksY1.Colour.Returns(foreColour);
            minorTickMarksY1.Length.Returns(2);
            minorTickMarksY1.Visible.Returns(true);

            tickLabelsY1 = Substitute.For<ITickLabelSettings>();
            tickLabelsY1.Colour.Returns(foreColour);
            tickLabelsY1.Font.Returns(font);
            tickLabelsY1.Visible.Returns(true);

            scaleY1 = Substitute.For<IScaleSettings>();
            scaleY1.MajorTickMarks.Returns(majorTickMarksY1);
            scaleY1.MajorTickMarks.Returns(minorTickMarksY1);
            scaleY1.TickLabels.Returns(tickLabelsY1);
            scaleY1.Colour.Returns(foreColour);
            scaleY1.Autoscale.Returns(false);
            scaleY1.Reversed.Returns(false);
            scaleY1.Visible.Returns(true);
            scaleY1.Maximum.Returns(10);
            scaleY1.Minimum.Returns(0);

            axisY1 = Substitute.For<IAxisSettings>();
            axisY1.Label.Returns(labelY1);
            axisY1.Scale.Returns(scaleY1);
            axisY1.Visible.Returns(false);

            labelY2 = Substitute.For<ILabelSettings>();
            labelY2.Colour.Returns(foreColour);
            labelY2.Font.Returns(font);

            majorTickMarksY2 = Substitute.For<ITickMarkSettings>();
            majorTickMarksY2.Colour.Returns(foreColour);
            majorTickMarksY2.Length.Returns(4);
            majorTickMarksY2.Visible.Returns(true);

            minorTickMarksY2 = Substitute.For<ITickMarkSettings>();
            minorTickMarksY2.Colour.Returns(foreColour);
            minorTickMarksY2.Length.Returns(2);
            minorTickMarksY2.Visible.Returns(true);

            tickLabelsY2 = Substitute.For<ITickLabelSettings>();
            tickLabelsY2.Colour.Returns(foreColour);
            tickLabelsY2.Font.Returns(font);
            tickLabelsY2.Visible.Returns(true);

            scaleY2 = Substitute.For<IScaleSettings>();
            scaleY2.MajorTickMarks.Returns(majorTickMarksY2);
            scaleY2.MajorTickMarks.Returns(minorTickMarksY2);
            scaleY2.TickLabels.Returns(tickLabelsY2);
            scaleY2.Colour.Returns(foreColour);
            scaleY2.Autoscale.Returns(false);
            scaleY2.Reversed.Returns(false);
            scaleY2.Visible.Returns(true);
            scaleY2.Maximum.Returns(10);
            scaleY2.Minimum.Returns(0);

            axisY2 = Substitute.For<IAxisSettings>();
            axisY2.Label.Returns(labelY2);
            axisY2.Scale.Returns(scaleY2);
            axisY2.Visible.Returns(false);
            
            axes = Substitute.For<IAxesSettings>();
            axes.Colour.Returns(foreColour);
            axes.Font.Returns(font);
            axes.X1.Returns(axisX1);
            axes.X2.Returns(axisX2);
            axes.Y1.Returns(axisY1);
            axes.Y2.Returns(axisY2);
            
            grid = Substitute.For<IGridSettings>();
            grid.MajorGridLines.Returns(majorGridLines);
            grid.MinorGridLines.Returns(minorGridLines);
            grid.Colour.Returns(foreColour);
            grid.Visible.Returns(true);

            plotArea = Substitute.For<IPlotAreaSettings>();
            plotArea.BackColour.Returns(backColour);
            plotArea.ForeColour.Returns(foreColour);
            plotArea.Grid.Returns(grid);
            grid.Visible.Returns(true);

            title = Substitute.For<ILabelSettings>();
            title.Colour.Returns(foreColour);
            title.Visible.Returns(true);
            title.Font.Returns(font);

            chart = Substitute.For<IChartSettings>();
            chart.BackColour.Returns(backColour);
            chart.ForeColour.Returns(foreColour);
            chart.PlotArea.Returns(plotArea);
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
        /// <param name="colour">The chart title colour.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="IChartSettings"/>.</returns>
        public ChartSettingsBuilder AddTitle(string text, string colour)
        {
            title.Colour.Returns(colour);
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
