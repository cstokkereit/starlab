using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// Defines the methods required to execute the use cases that implement the table settings panel functionality.
    /// </summary>
    public interface ITableSettingsUseCaseService
    {
        /// <summary>
        /// Executes the UpdateTable use case.
        /// </summary>
        /// <param name="id">The ID of the table view controller.</param>
        /// <param name="chart">A <see cref="ITableSettings"/> that specifies the current state of the table.</param>
        void UpdateTable(DocumentID id, ITableSettings table);

        /// <summary>
        /// Executes the UpdateDocument use case.
        /// </summary>
        /// <param name="workspace">A <see cref="IWorkspace"/> that specifies the current state of the workspace.</param>
        /// <param name="id">The ID of the chart view controller.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        void UpdateDocument(IWorkspace workspace, DocumentID id, IChartSettings chart);
    }
}
