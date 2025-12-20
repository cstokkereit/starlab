namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a visual element that is part of a chart.
    /// </summary>
    internal abstract class ChartElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartElement"> class.
        /// </summary>
        /// <param name="visible">Specifies whether this chart element is visble or not.</param>
        public ChartElement(bool visible)
        {
            Visible = visible;
        }

        /// <summary>
        /// A flag indicating that the chart element is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
