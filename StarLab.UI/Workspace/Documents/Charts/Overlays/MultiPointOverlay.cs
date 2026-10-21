using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// TODO : Documentation
    /// </summary>
    internal class MultiPointOverlay : Overlay
    {
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
