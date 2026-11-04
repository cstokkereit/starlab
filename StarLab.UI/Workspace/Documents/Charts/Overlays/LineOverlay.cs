using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A chart overlay that consists of a single line.
    /// </summary>
    internal class LineOverlay : Overlay
    {
        private readonly double[] xs; // The x coordinates of the points on the line.

        private readonly double[] ys; // The y coordinates of the points on the line.

        private readonly string label; // The label text.

        /// <summary>
        /// Initialises a new instance of the <see cref="LineOverlay"> class.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        /// <param name="name">The overlay name.</param>
        /// <param name="x">The x coordinates of the points on the line.</param>
        /// <param name="y">The y coordinates of the points on the line.</param>
        /// <param name="text">The label text.</param>
        public LineOverlay(Plot plot, string name, double[] xs, double[] ys, string label)
            : base(plot, name)
        {
            this.label = label;
            this.xs = xs;
            this.ys = ys;
        }

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
