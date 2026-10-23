using ScottPlot;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents.Charts.Overlays;
using StarLab.Shared;
using StarLab.UI.Workspace.Documents.Charts.Overlays;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A factory for creating overlays.
    /// </summary>
    internal class OverlayFactory : Factory
    {
        private Plot? plot; // The plot that will display the overlays.

        /// <summary>
        /// Initialises a new instance of the <see cref="OverlayFactory"/> class.
        /// </summary>
        /// <param name="configuration">The type configuration information.</param>
        public OverlayFactory(Plot plot, IFactoryConfiguration configuration)
            : base(configuration) 
        {
            this.plot = plot;
        }

        /// <summary>
        /// Creates the specified <see cref="Overlay"/>.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="x">The x coordinate of the labelled point.</param>
        /// <param name="y">The y coordinate of the labelled point.</param>
        /// <param name="text">The label text.</param>
        /// <returns>The specified <see cref="IOverlay"/>.</returns>
        public Overlay CreatePointOverlay(string name, double x, double y, string text)
        {
            if (plot == null) throw new InvalidOperationException(ExceptionMessages.ObjectNotInitialised(typeof(OverlayFactory)));

            return (Overlay)CreateInstance(Configuration.GetOverlayConfiguration(name).TypeName, [plot, name, x, y, text]);
        }
    }
}
