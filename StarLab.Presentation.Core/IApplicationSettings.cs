namespace StarLab.Presentation
{
    /// <summary>
    /// Provides access to the application settings defined in the App.Config file.
    /// </summary>
    public interface IApplicationSettings
    {
        /// <summary>
        /// Gets or sets the path to the default workspace file.
        /// </summary>
        string Workspace { get; set; }
    }
}
