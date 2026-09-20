namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChartViewPresenter"/> to control the behaviour of a chart.
    /// </summary>
    public interface IChartView : IChildView
    {
        /// <summary>
        /// Updates the chart following a change to the chart data.
        /// </summary>
        /// <param name="config">An <see cref="IChart"/> used to configure the chart.</param>
        /// <param name="data">An <see cref="IChartData"> that holds data that will be used to generate the chart.</param>
        void UpdateChart(IChart config, IChartData data);

        /// <summary>
        /// Updates the chart following a change to the chart configuration.
        /// </summary>
        /// <param name="config">The <see cref="IChart"/> used to configure the chart.</param>
        void UpdateChart(IChart config);
    }
}
