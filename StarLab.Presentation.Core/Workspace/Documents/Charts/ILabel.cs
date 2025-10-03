namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a chart label.
    /// </summary>
    public interface ILabel
    {
        /// <summary>
        /// Gets the background colour.
        /// </summary>
        string BackColour { get; }

        /// <summary>
        /// Gets the label font.
        /// </summary>
        IFont Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        string ForeColour { get; }

        /// <summary>
        /// Gets the label text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets a flag indicating whether the label is visible.
        /// </summary>
        bool Visible { get; }
    }
}
