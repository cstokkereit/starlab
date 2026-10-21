namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents a factory for creating overlays.
    /// </summary>
    public interface IOverlayFactory
    {
        /// <summary>
        /// Creates the specified <see cref="IOverlay"/>.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="x">The x coordinate of the labelled point.</param>
        /// <param name="y">The y coordinate of the labelled point.</param>
        /// <param name="text">The label text.</param>
        /// <returns>The specified <see cref="IOverlay"/>.</returns>
        IOverlay CreatePointOverlay(string name, double x, double y, string text);
    }
}
