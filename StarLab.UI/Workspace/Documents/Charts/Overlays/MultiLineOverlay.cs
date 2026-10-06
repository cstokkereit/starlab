using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    internal class MultiLineOverlay
    {
        private readonly List<LineOverlay> lines = new List<LineOverlay>();

        private ScottPlot.Color colour;

        public ScottPlot.Color Colour 
        { 
            get => colour;

            set
            {
                foreach (var line in lines)
                {
                    line.Colour = value;
                }

                colour = value;
            }
        }

        public bool Visible { get; set; }

        public void AddLine(double[] xs, double[] ys, string label)
        {
            lines.Add(new LineOverlay(xs, ys, label));
        }

        public void AddLine(double[] xs, double[] ys)
        {
            lines.Add(new LineOverlay(xs, ys));
        }

        public void Render(Plot plot)
        {
            if (Visible)
            {
                foreach (var line in lines)
                {
                    line.Render(plot);
                }
            }
        }
    }
}
