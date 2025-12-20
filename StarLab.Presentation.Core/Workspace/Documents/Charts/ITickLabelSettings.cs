namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the tick marks for an axis while the chart is being configured.
    /// </summary>
    public interface ITickLabelSettings : ITextElementSettings
    {
        /// <summary>
        /// Gets or sets the angle of rotation for the tick labels.
        /// </summary>
        int Rotation { get; set; }
    }
}
