namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChartViewPresenter"/> to control the behaviour of a chart.
    /// </summary>
    public interface IChartView : IChildView
    {
        /// <summary>
        /// Updates the state of the chart following a change.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the new state of the chart.</param>
        void UpdateChart(IChart chart);
    }
}
