using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A chart overlay that consists of multiple points.
    /// </summary>
    internal class MultiPointOverlay : Overlay
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MultiPointOverlay"> class.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        /// <param name="name">The overlay name.</param>
        public MultiPointOverlay(Plot plot, string name)
            : base(plot, name) { }

        /// <summary>
        /// Removes the elements that compose the overlay from the chart.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        protected override void Clear(Plot plot)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Renders the overlay on the specified plot.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        protected override void Render(Plot plot)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the elements that compose the overlay.
        /// </summary>
        protected override void UpdateElements()
        {
            throw new NotImplementedException();
        }
    }
}
