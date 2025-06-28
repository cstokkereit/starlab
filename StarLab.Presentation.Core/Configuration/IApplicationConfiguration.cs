namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Represents the application configuration.
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets the default workspace.
        /// </summary>
        string Workspace { get; set; }
    }
}
