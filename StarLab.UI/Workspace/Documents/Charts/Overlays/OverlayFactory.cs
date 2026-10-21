using ScottPlot;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents.Charts.Overlays;
using StarLab.Shared;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A factory for creating overlays.
    /// </summary>
    internal class OverlayFactory : Factory, IOverlayFactory
    {
        private Plot? plot; // The plot that will display the overlays.

        /// <summary>
        /// Initialises a new instance of the <see cref="OverlayFactory"/> class.
        /// </summary>
        /// <param name="configuration">The type configuration information.</param>
        public OverlayFactory(IFactoryConfiguration configuration)
            : base(configuration) { }

        /// <summary>
        /// TODO : Documentation
        /// </summary>
        /// <param name="plot"></param>
        public void Attach(Plot plot)
        {
            this.plot = plot;
        }

        /// <summary>
        /// Creates the specified <see cref="IOverlay"/>.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="x">The x coordinate of the labelled point.</param>
        /// <param name="y">The y coordinate of the labelled point.</param>
        /// <param name="text">The label text.</param>
        /// <returns>The specified <see cref="IOverlay"/>.</returns>
        public IOverlay CreatePointOverlay(string name, double x, double y, string text)
        {
            if (plot == null) throw new InvalidOperationException(ExceptionMessages.ObjectNotInitialised(typeof(OverlayFactory)));

            return (IOverlay)CreateInstance(Configuration.GetOverlayConfiguration(name).TypeName, [plot, x, y, text]);
        }
    }
}
