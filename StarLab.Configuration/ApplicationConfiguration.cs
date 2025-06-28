using log4net;
using StarLab.Application;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;

namespace StarLab.Configuration
{
    /// <summary>
    /// The application configuration.
    /// </summary>
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ApplicationConfiguration)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, IViewConfiguration> views = new Dictionary<string, IViewConfiguration>(); // A dictionary containing the view configurations indexed by name.

        private readonly IApplicationSettings settings; // Provides access to the application settings defined in the App.config file.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationConfiguration"/> class.
        /// </summary>
        /// <param name="settings">Provides access to the application settings.</param>
        /// <param name="serialiser">An <see cref="ISerialisationProvider"/> that will be used to serialise the <see cref="ViewConfiguration"/>.</param> TODO
        /// <exception cref="ArgumentNullException"></exception>
        public ApplicationConfiguration(IApplicationSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Gets or sets the path to the default workspace.
        /// </summary>
        public string Workspace
        {
            get { return settings.Workspace; }
            set { settings.Workspace = value; }
        }
    }
}
