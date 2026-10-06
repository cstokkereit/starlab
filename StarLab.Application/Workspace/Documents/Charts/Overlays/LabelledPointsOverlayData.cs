namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// The data required to generate an overlay that contains one or more labelled points.
    /// </summary>
    internal class LabelledPointsOverlayData : OverlayData, ILabelledPointsOverlayData
    {
        private readonly List<string> labels = new List<string>(); // A list containing the labels for the points in the overlay.

        /// <summary>
        /// Gets a <see cref="string[]"/> containing the labels for the points in the overlay.
        /// </summary>
        public string[] Labels => labels.ToArray();

        /// <summary>
        /// Adds a label for a point in the overlay.
        /// </summary>
        /// <param name="label">The label text.</param>
        public void AddLabel(string label)
        {
            labels.Add(label);
        }
    }
}
