namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart axis scale tick marks.
    /// </summary>
    public interface ITickMarks : IFrameElement
    {
        /// <summary>
        /// Gets the length of the tickamrks.
        /// </summary>
        public int Length { get; }
    }
}
