namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Defines the methods required to execute the use cases that implement the chart overlays panel functionality.
    /// </summary>
    public interface IChartOverlaysUseCaseService
    {
        /// <summary>
        /// Executes the AddChartOverlay use case.
        /// </summary>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <param name="name">The name of the overlay.</param>
        void AddChartOverlay(DocumentID id, IChartSettings chart, string name);

        /// <summary>
        /// Executes the RemoveChartOverlay use case.
        /// </summary>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <param name="name">The name of the overlay.</param>
        void RemoveChartOverlay(DocumentID id, IChartSettings chart, string name);

        /// <summary>
        /// Executes the UpdateChart use case.
        /// </summary>
        /// <param name="workspace">A <see cref="IWorkspace"/> that specifies the current state of the workspace.</param>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        void UpdateChart(IWorkspace workspace, DocumentID id, IChartSettings chart);
    }
}
