using static log4net.Appender.ColoredConsoleAppender;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a visual element that is part of a chart.
    /// </summary>
    internal abstract class ChartElement : IChartElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartElement"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the chart element.</param>
        /// <param name="visible">Specifies whether the chart element is visble or not.</param>
        public ChartElement(string colour, bool visible)
        {
            Visible = visible;
            Colour = colour;
        }

        /// <summary>
        /// Gets the colour of the chart element.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// A flag indicating that the chart element is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
