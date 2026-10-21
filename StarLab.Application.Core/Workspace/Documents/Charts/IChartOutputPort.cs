namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Used by a <see cref="UseCase{TOutputPort}"/> to update the document.
    /// </summary>
    public interface IChartOutputPort : IOutputPort
    {
        /// <summary>
        /// Adds the specified overlay to the chart.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="data">The overlay data.</param>
        void AddOverlay(string name, LabelledPointDTO data);

        /// <summary>
        /// Applies the new chart settings to the preview.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the state of the chart.</param>
        void ApplyChanges(ChartDTO dto);

        /// <summary>
        /// Plots the chart.
        /// </summary>
        /// <param name="stars">A <see cref="List{StarDTO}"> containing the chart data.</param>
        void PlotChart(List<StarDTO> stars);

        /// <summary>
        /// Removes the specified overlay from the chart.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        void RemoveOverlay(string name);
    }
}
