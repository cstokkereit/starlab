namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents a data provider for chart overlays.
    /// </summary>
    public interface IOverlayDataProvider
    {
        /// <summary>
        /// Gets the data for the magnitude classes overlay.
        /// </summary>
        /// <returns>An <see cref="IMagnitudeClassesOverlayData"/> that contains the overlay data.</returns>
        IMagnitudeClassesOverlayData GetMagnitudeClassesOverlayData();

        /// <summary>
        /// Gets the data for the named stars overlay.
        /// </summary>
        /// <returns>An <see cref="ILabelledPointsOverlayData"/> that contains the overlay data.</returns>
        ILabelledPointsOverlayData GetNamedStarsOverlayData();
    }
}
