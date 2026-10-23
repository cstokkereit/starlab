using StarLab.Presentation.Options;

namespace StarLab.UI.Options
{
    /// <summary>
    /// Provides read/write access to the chart settings.
    /// </summary>
    internal class ChartSettings : IChartSettings
    {
        /// <summary>
        /// Gets the overlays settings.
        /// </summary>
        public IOverlaySettings Overlays => new OverlaySettings();
    }
}
