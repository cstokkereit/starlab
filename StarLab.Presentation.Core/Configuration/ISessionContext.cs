namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Provides contextual information about the current session, including application configuration and user settings.
    /// </summary>
    public interface ISessionContext
    {
        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        IApplicationConfiguration Configuration { get; }

        /// <summary>
        /// Gets the user settings.
        /// </summary>
        IUserSettings Settings { get; }
    }
}
