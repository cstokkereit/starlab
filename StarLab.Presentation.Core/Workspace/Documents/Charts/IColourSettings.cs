namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart element while the chart is being configured.
    /// </summary>
    public interface IColourSettings
    {
        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        string ForeColour { get; set; }
    }
}
