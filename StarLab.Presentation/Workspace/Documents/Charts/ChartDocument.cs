using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents.Tables;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart document in the workspace hierarchy.
    /// </summary>
    internal class ChartDocument : Document, IChartDocument
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartDocument"/>.
        /// </summary>
        /// <param name="dto">A <see cref="DocumentDTO"/> representation of the document.</param>
        public ChartDocument(DocumentDTO dto)
            : base(dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Chart = dto.Chart != null ? new Chart(dto.Chart) : new Chart();
        }

        /// <summary>
        /// Gets the chart.
        /// </summary>
        public IChart Chart { get; }
    }
}
