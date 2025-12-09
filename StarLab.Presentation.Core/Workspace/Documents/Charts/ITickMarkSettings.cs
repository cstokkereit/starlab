namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the tick marks for an axis while the chart is being configured.
    /// </summary>
    public interface ITickMarkSettings : IColourSettings, IVisibilitySettings
    {
        /// <summary>
        /// Gets or sets the length of the tickamrks.
        /// </summary>
        public int Length { get; set; }
    }
}
