namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the available colour settings.
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
