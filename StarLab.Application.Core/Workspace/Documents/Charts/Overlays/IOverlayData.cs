namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents the data required to generate an overlay.
    /// </summary>
    public interface IOverlayData
    {
        /// <summary>
        /// Gets a <see cref="double[]"/> containing the ordered values that will be used to specify the location of points with respect to one of the coordinate axes.
        /// </summary>
        /// <param name="field">The name of the field that contains the data.</param>
        /// <returns>A <see cref="double[]"/> containing the specified values.</returns>
        double[] GetValues(string field);
    }
}
