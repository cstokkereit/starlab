namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a controller that can be used to control a chart.
    /// </summary>
    public interface IChartController : IChildViewController
    {
        /// <summary>
        /// Gets the current state of the chart.
        /// </summary>
        IChart? Chart { get; }

        /// <summary>
        /// Reverts the changes made to the preview.
        /// </summary>
        void RevertChanges();

        /// <summary>
        /// Updates the view with the new <see cref="IChart"/> definition following a change to the document or workspace.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the state of the chart.</param>
        void UpdateChart(IChart chart);

        
    }
}
