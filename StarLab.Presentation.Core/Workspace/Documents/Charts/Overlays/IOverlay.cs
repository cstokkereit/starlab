namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents a chart overlay.
    /// </summary>
    public interface IOverlay : IChartElement
    {
        /// <summary>
        /// Gets the overlay font.
        /// </summary>
        IFont Font { get; }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        string Name { get; }
    }
}
