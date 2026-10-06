namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents the data required to generate an overlay that contains one or more labelled points.
    /// </summary>
    public interface ILabelledPointsOverlayData : IOverlayData
    {
        /// <summary>
        /// Gets a <see cref="string[]"/> containing the labels for the points in the overlay.
        /// </summary>
        string[] Labels { get; }
    }
}
