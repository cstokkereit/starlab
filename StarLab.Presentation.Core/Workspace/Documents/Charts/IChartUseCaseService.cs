namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the methods required to execute the use cases that implement the chart functionality.
    /// </summary>
    public interface IChartUseCaseService : IUseCaseService
    {
        /// <summary>
        /// Executes the UpdateChart use case.
        /// </summary>
        /// <param name="workspace">The current <see cref="IWorkspace"/>.</param>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart"/>TODO</param>
        void PlotChart(IWorkspace workspace, DocumentID id, IChart chart);
    }
}
