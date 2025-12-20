namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a visual element that is part of a chart.
    /// </summary>
    public interface IChartElement
    {
        /// <summary>
        /// A flag indicating that the chart element is visible.
        /// </summary>
        bool Visible { get; }
    }
}
