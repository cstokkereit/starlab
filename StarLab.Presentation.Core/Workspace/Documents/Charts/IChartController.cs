namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a controller that can be used to control a chart.
    /// </summary>
    public interface IChartController : IChildViewController
    {
        /// <summary>
        /// Updates the view with the new <see cref="IChart"/> definition following a change to the document or workspace.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the state of the chart.</param>
        void UpdateChart(IChart chart);

        /// <summary>
        /// Reverts the preview to the old chart settings.
        /// </summary>
        void UpdatePreview();
    }
}
