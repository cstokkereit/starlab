namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents an overlay that contains magnitude class data.
    /// </summary>
    public interface IMagnitudeClassesOverlayData
    {
        /// <summary>
        /// Gets the overlay data for the specified magnitude class.
        /// </summary>
        /// <param name="name">The name of the magnitude class.</param>
        /// <returns>The overlay data for the specified magnitude class.</returns>
        IOverlayData GetData(string name);
    }
}
