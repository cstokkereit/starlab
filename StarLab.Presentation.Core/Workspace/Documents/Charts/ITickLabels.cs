namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart axis scale tick labels.
    /// </summary>
    public interface ITickLabels : ITextElement
    {
        /// <summary>
        /// Gets the rotation angle for the tick labels.
        /// </summary>
        int Rotation { get; }
    }
}
