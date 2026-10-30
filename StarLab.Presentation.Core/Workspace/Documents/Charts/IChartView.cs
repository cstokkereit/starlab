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
        /// Plots the chart.
        /// </summary>
        /// <param name="config">The chart configuration.</param>
        /// <param name="data">The chart data.</param>
        void PlotChart(IChart config, IChartData data);

        /// <summary>
        /// Updates the chart following a change to the chart configuration.
        /// </summary>
        /// <param name="config">The chart configuration.</param>
        void UpdateChart(IChart config);
    }
}
