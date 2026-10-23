namespace StarLab.Presentation.Options
{
    /// <summary>
    /// Represents the overlay settings.
    /// </summary>
    public interface IChartSettings
    {
        /// <summary>
        /// Gets the overlays settings.
        /// </summary>
        IOverlaySettings Overlays { get; }
    }
}
