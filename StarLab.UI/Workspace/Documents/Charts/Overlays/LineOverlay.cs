using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// TODO : Documentation
    /// </summary>
    internal class LineOverlay : Overlay
    {
        private readonly string label;

        private readonly double[] xs;

        private readonly double[] ys;

        public LineOverlay(Plot plot, string name, double[] xs, double[] ys, string label)
            : base(plot, name)
        {
            this.label = label;
            this.xs = xs;
            this.ys = ys;
        }

        //public LineOverlay(double[] xs, double[] ys)
        //    : this(xs, ys, string.Empty) { }


        /// <summary>
        /// Removes the elements that compose the overlay from the chart.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        protected override void Clear(Plot plot)
        {
            throw new NotImplementedException();
        }

        protected override void Render(Plot plot)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateElements()
        {
            throw new NotImplementedException();
        }
    }
}
