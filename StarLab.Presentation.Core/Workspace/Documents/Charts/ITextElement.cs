namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a text element that is part of a chart.
    /// </summary>
    public interface ITextElement : IFrameElement
    {
        /// <summary>
        /// Gets the font for the text element.
        /// </summary>
        IFont Font { get; }
    }
}
