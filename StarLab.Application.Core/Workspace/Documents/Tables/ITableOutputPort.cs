namespace StarLab.Application.Workspace.Documents.Tables
{
    /// <summary>
    /// Used by a <see cref="UseCaseInteractor{TOutputPort}"/> to update the document.
    /// </summary>
    public interface ITableOutputPort : IOutputPort
    {
        /// <summary>
        /// Applies the new table settings to the preview.
        /// </summary>
        /// <param name="dto">A <see cref="TableDTO"/> that specifies the state of the table.</param>
        void UpdatePreview(TableDTO dto);
    }
}
