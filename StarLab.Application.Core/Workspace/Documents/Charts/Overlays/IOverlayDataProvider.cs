using StarLab.Application.Data;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents a data provider for chart overlays.
    /// </summary>
    public interface IOverlayDataProvider
    {
        /// <summary>
        /// Gets the data for the specified overlay.
        /// </summary>
        /// <param name="filename">The name of the file containing the overlay data.</param>param>
        /// <returns>An <see cref="IMagnitudeClassesOverlayData"/> that contains the overlay data.</returns>
        IMagnitudeClassesOverlayData GetMagnitudeClassesOverlayData();

        /// <summary>
        /// Gets the data for the specified overlay.
        /// </summary>
        /// <param name="filename">The name of the file containing the overlay data.</param>param>
        /// <returns>An <see cref="ILabelledPointsOverlayData"/> that contains the overlay data.</returns>
        ILabelledPointsOverlayData GetMultiplePointsOverlayData(string filename);

        /// <summary>
        /// Gets the data for the specified overlay.
        /// </summary>
        /// <returns>An <see cref="ILabelledPoint"/> that contains the data for the sun.</returns>
        ILabelledPoint GetPointOverlayData();
    }
}
