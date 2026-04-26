namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Provides contextual information about the current session, including application configuration and user settings.
    /// </summary>
    public class SessionContext : ISessionContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SessionContext"/> class.
        /// </summary>
        /// <param name="configuration">An <see cref="IApplicationConfiguration"/> that holds the application specific configuration.</param>
        /// <param name="settings">An <see cref="IUserSettings"/> that holds the user specific settings defined in the app.config file.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SessionContext(IApplicationConfiguration configuration, IUserSettings settings)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        public IApplicationConfiguration Configuration { get; }

        /// <summary>
        /// Gets the user settings.
        /// </summary>
        public IUserSettings Settings { get; }
    }
}
