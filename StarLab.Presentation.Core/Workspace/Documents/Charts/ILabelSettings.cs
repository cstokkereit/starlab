namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a label while the chart is being configured.
    /// </summary>
    public interface ILabelSettings : ITextElementSettings
    {
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        string Text { get; set; }
    }
}
