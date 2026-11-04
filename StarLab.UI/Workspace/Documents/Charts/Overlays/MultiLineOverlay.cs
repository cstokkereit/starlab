using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A chart overlay that consists of multiple lines.
    /// </summary>
    internal class MultiLineOverlay : Overlay
    {
        private readonly List<LineOverlay> lines = new List<LineOverlay>(); // A list containing the lines that are included in the overlay.

        /// <summary>
        /// Initialises a new instance of the <see cref="MultiLineOverlay"> class.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        /// <param name="name">The overlay name.</param>
        public MultiLineOverlay(Plot plot, string name)
            : base(plot, name) { }

        /// <summary>
        /// Adds a labelled line to the overlay.
        /// </summary>
        /// <param name="x">The x coordinates of the points on the line.</param>
        /// <param name="y">The y coordinates of the points on the line.</param>
        /// <param name="text">The label text.</param>
        public void AddLine(double[] xs, double[] ys, string label)
        {
            //lines.Add(new LineOverlay(xs, ys, label));
        }

        /// <summary>
        /// Adds a line to the overlay.
        /// </summary>
        /// <param name="x">The x coordinates of the points on the line.</param>
        /// <param name="y">The y coordinates of the points on the line.</param>
        public void AddLine(double[] xs, double[] ys)
        {
            //AddLine(xs, ys, string.Empty);
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
