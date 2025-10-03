namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart element while the chart is being configured.
    /// </summary>
    public interface IVisibilitySettings
    {
        /// <summary>
        /// Gets or sets a flag that determines whether the chart element is visible.
        /// </summary>
        bool Visible { get; set; }
    }
}
