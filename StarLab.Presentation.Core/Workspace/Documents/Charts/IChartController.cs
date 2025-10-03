namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a controller that can be used to control a chart.
    /// </summary>
    public interface IChartController : IChildViewController
    {
        /// <summary>
        /// Updates the view with the new <see cref="IChart"/> definition following a change to the document or workspace. This will replace the current chart definition.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the state of the chart.</param>
        void UpdateChart(IChart chart);

        /// <summary>
        /// Updates the view with the current chart definition. This will revert any preview changes.
        /// </summary>
        void UpdateChart();
    }
}
