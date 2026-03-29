namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart document.
    /// </summary>
    public interface IChartDocument : IDocument
    {
        /// <summary>
        /// Gets the chart.
        /// </summary>
        IChart Chart { get; }
    }
}
