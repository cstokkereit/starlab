namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// The data required to generate an overlay that shows how luminosity varies with effective temperature for the different magnitude classes.
    /// </summary>
    internal class MagnitudeClassesOverlayData : IMagnitudeClassesOverlayData
    {
        /// <summary>
        /// Gets an <see cref="IOverlayData"/> containing the data for the specified magnitude class.
        /// </summary>
        /// <param name="name">The name of the magnitude class.</param>
        /// <returns>An <see cref="IOverlayData"/> containing the data for the specified magnitude class.</returns>
        public IOverlayData GetData(string name)
        {
            return new OverlayData();
        }
    }
}
