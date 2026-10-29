namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents the current state of the chart overlays while the chart is being configured.
    /// </summary>
    public interface IOverlayGroupSettings : IChartElementSettings, IFontSettings, IReadOnlyCollection<IOverlaySettings>
    {
        /// <summary>
        /// Gets the specified chart overlay settings.
        /// </summary>
        /// <param name="name">The name of the chart overlay.</param>
        /// <returns>The settings for the specified chart overlay.</returns>
        IOverlaySettings this[string name] { get; }
    }
}
