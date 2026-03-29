namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Used by a <see cref="UseCaseInteractor{TOutputPort}"/> to update the document.
    /// </summary>
    public interface IChartOutputPort : IOutputPort
    {
        /// <summary>
        /// Applies the new chart settings to the preview.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the state of the chart.</param>
        void UpdatePreview(ChartDTO dto);
    }
}
