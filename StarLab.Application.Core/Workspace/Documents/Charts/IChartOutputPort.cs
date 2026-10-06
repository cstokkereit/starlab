namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Used by a <see cref="UseCaseInteractor{TOutputPort}"/> to update the document.
    /// </summary>
    public interface IChartOutputPort : IOutputPort
    {
        /// <summary>
        /// Sets the chart data.
        /// </summary>
        /// <param name="stars">A <see cref="List{StarDTO}"> containing the chart data.</param>
        void SetData(List<StarDTO> stars);


        /// <summary>
        /// Applies the new chart settings to the preview.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the state of the chart.</param>
        void UpdatePreview(ChartDTO dto);
    }
}
