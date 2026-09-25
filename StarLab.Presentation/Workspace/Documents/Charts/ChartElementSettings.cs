namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart element while the chart is being configured.
    /// </summary>
    internal abstract class ChartElementSettings : IChartElementSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartElementSettings"/> class.
        /// </summary>
        /// <param name="element">An <see cref="IChartElement"/> that specifies the initial state of the chart element.</param>
        public ChartElementSettings(IChartElement element)
        {
            Visible = element.Visible;
            Colour = element.Colour;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartElementSettings"/> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the foreground colour of the chart element.</param>
        /// <param name="visible">Specifies whether the chart element is visble or not.</param>
        public ChartElementSettings(string colour, bool visible)
        {
            Visible = visible;
            Colour = colour;
        }

        /// <summary>
        /// Gets or sets the colour of the chart element.
        /// </summary>
        public virtual string Colour { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating that the chart element is visible.
        /// </summary>
        public virtual bool Visible { get; set; }
    }
}
