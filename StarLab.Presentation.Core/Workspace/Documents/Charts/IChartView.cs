namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IChartViewPresenter"/> to control the behaviour of a chart.
    /// </summary>
    public interface IChartView : IChildView
    {
        /// <summary>
        /// Adds the specified point overlay to the chart.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="text">The label text.</param>
        void AddOverlay(string name, double x, double y, string text);

        /// <summary>
        /// Removes the specified overlay from the chart.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        void RemoveOverlay(string name);

        /// <summary>
        /// Updates the chart following a change to the chart data.
        /// </summary>
        /// <param name="config">An <see cref="IChart"/> used to configure the chart.</param>
        /// <param name="data">An <see cref="IChartData"> that holds data that will be used to generate the chart.</param>
        void PlotChart(IChart config, IChartData data);

        /// <summary>
        /// Updates the chart following a change to the chart configuration.
        /// </summary>
        /// <param name="config">The <see cref="IChart"/> used to configure the chart.</param>
        void UpdateChart(IChart config);
    }
}
