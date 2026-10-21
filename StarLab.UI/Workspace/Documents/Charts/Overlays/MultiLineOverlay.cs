using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// TODO : Documentation
    /// </summary>
    internal class MultiLineOverlay : Overlay
    {
        private readonly List<LineOverlay> lines = new List<LineOverlay>();

        public MultiLineOverlay(Plot plot, string name)
            : base(plot, name) { }

        public void AddLine(double[] xs, double[] ys, string label)
        {
            //lines.Add(new LineOverlay(xs, ys, label));
        }

        public void AddLine(double[] xs, double[] ys)
        {
            //lines.Add(new LineOverlay(xs, ys));
        }

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
