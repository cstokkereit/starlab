using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart axis scale tick marks.
    /// </summary>
    public interface ITickMarks : IChartElement
    {
        /// <summary>
        /// Gets the length of the tickamrks.
        /// </summary>
        int Length { get; }
    }
}
