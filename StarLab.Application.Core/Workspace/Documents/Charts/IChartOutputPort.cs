namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Used by a <see cref="UseCaseInteractor{TOutputPort}"/> to update the document.
    /// </summary>
    public interface IChartOutputPort : IOutputPort
    {
        /// <summary>
        /// Updates the view with the preview chart definition. This does not replace the current chart definition.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the state of the chart.</param>
        void UpdateChart(ChartDTO dto);
    }
}
