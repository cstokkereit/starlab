using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents the current state of a chart overlay while the chart is being configured.
    /// </summary>
    public interface IOverlaySettings : IChartElementSettings, IFontSettings
    {
        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the overlay type.
        /// </summary>
        OverlayType Type { get; }
    }
}
