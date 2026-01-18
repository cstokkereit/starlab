namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the grid lines while the chart is being configured.
    /// </summary>
    public interface IGridLineSettings : IFrameElementSettings
    {
        /// <summary>
        /// Gets or sets the opacity of the grid lines.
        /// </summary>
        double Opacity { get; set; }

    }
}
