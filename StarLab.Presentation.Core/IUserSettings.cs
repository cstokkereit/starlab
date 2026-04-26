namespace StarLab.Presentation
{
    /// <summary>
    /// Represents the user settings defined in the app.Config file.
    /// </summary>
    public interface IUserSettings
    {
        /// <summary>
        /// Gets or sets the path to the default workspace file.
        /// </summary>
        string Workspace { get; set; }
    }
}
