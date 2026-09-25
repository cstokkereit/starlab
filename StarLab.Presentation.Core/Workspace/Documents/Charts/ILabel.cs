namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a label that is part of a chart.
    /// </summary>
    public interface ILabel : IChartElement
    {
        /// <summary>
        /// Gets the label font.
        /// </summary>
        IFont Font { get; }

        /// <summary>
        /// Gets the label text.
        /// </summary>
        string Text { get; }
    }
}
