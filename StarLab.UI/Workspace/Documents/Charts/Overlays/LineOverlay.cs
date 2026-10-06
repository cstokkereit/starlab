using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    internal class LineOverlay
    {
        private readonly string label;

        private readonly double[] xs;

        private readonly double[] ys;

        public LineOverlay(double[] xs, double[] ys, string label)
        {
            this.label = label;
            this.xs = xs;
            this.ys = ys;
        }

        public LineOverlay(double[] xs, double[] ys)
            : this(xs, ys, string.Empty) { }

        public ScottPlot.Color Colour { get; set; }

        public bool Visible { get; set; }

        
        public void Render(Plot plot)
        {
            plot.Add.ScatterLine(xs, ys, Colour);
        }
    }
}
