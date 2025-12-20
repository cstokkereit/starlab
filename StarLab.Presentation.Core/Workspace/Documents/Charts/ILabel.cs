namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart label.
    /// </summary>
    public interface ILabel : ITextElement
    {
        /// <summary>
        /// Gets the label text.
        /// </summary>
        string Text { get; }
    }
}
