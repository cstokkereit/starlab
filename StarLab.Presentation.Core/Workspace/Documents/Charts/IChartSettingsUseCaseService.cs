namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the methods required to execute the use cases that implement the chart settings panel functionality.
    /// </summary>
    public interface IChartSettingsUseCaseService : IUseCaseService
    {
        /// <summary>
        /// Executes the UpdateChart use case.
        /// </summary>
        /// <param name="id">The ID of the document that contains the chart.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        void UpdateChart(string id, IChartSettings chart);

        /// <summary>
        /// Executes the UpdateDocument use case.
        /// </summary>
        /// <param name="workspace">A <see cref="IWorkspace"/> that specifies the current state of the workspace.</param>
        /// <param name="id">The ID of the document that contains the chart.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        void UpdateDocument(IWorkspace workspace, string id, IChartSettings chart);
    }
}
