using ScottPlot;
using ScottPlot.Plottables;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A chart overlay that contains a single labelled point.
    /// </summary>
    internal class PointOverlay : Overlay
    {
        private readonly double x; // The x coordinate of the labelled point.

        private readonly double y; // The y coordinate of the labelled point.

        private readonly string text; // The label text.

        private Scatter? points; //  The plottable points used to render the overlay.

        private Text? label; // The plottable text used to render the overlay.

        /// <summary>
        /// Initialises a new instance of the <see cref="PointOverlay"> class.
        /// </summary>
        /// <param name="plot">The <see cref="Plot"/> that displays the overlay.</param>
        /// <param name="name">The overlay name.</param>
        /// <param name="x">The x coordinate of the labelled point.</param>
        /// <param name="y">The y coordinate of the labelled point.</param>
        /// <param name="text">The label text.</param>
        public PointOverlay(Plot plot, string name, double x, double y, string text)
            : base(plot, name) 
        {
            this.text = text;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Removes the elements that compose the overlay from the chart.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        protected override void Clear(Plot plot)
        {
            if (points != null)
            {
                plot.Remove(points);
                points = null;
            }

            if (label != null)
            {
                plot.Remove(label);
                label = null;
            }
        }

        /// <summary>
        /// Renders the overlay on the specified <see cref="Plot"/>.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        protected override void Render(Plot plot)
        {
            if (points == null)
            {
                points = plot.Add.ScatterPoints(new[] { new Coordinates(x, y) }, Colour);

                points.MarkerShape = MarkerShape.FilledCircle; // TODO Point size and shape 
                points.MarkerSize = 3;
            }
            
            if (label == null)
            {
                label = plot.Add.Text(text, new Coordinates(x, y));

                label.LabelFontColor = Colour;
                label.LabelFontName = Font.FontFamily.Name;
                label.LabelFontSize = Font.Size;
            }
        }

        /// <summary>
        /// Updates the elements that compose the overlay.
        /// </summary>
        protected override void UpdateElements()
        {
            if (label != null)
            {
                label.LabelFontName = Font.FontFamily.Name;
                label.LabelFontSize = Font.Size;
                label.LabelFontColor = Colour;
                label.IsVisible = Visible;
            }

            if (points != null)
            {
                //points.MarkerShape = MarkerShape.FilledCircle; // TODO Point size and shape 
                //points.MarkerSize = 3;

                points.IsVisible = Visible;
                points.Color = Colour;
            }
        }
    }
}
