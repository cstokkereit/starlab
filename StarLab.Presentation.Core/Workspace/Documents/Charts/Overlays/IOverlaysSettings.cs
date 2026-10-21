namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents the current state of the chart overlays while the chart is being configured.
    /// </summary>
    public interface IOverlaysSettings : IChartElementSettings, IFontSettings, IReadOnlyDictionary<string, IOverlaySettings>
    {
        void Add(string name);
    }
}
