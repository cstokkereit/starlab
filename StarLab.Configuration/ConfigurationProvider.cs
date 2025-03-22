using Microsoft.Extensions.Configuration;
using StarLab.Configuration.Serialisation;
using StarLab.Presentation.Configuration;
using System.Diagnostics;

namespace StarLab.Configuration
{
    /// <summary>
    /// The application configuration.
    /// </summary>
    public class ConfigurationProvider : Presentation.Configuration.IConfigurationProvider
    {
        private readonly Dictionary<string, IViewConfiguration> views = new Dictionary<string, IViewConfiguration>(); // A dictionary containing the view configurations indexed by name.

        private IConfiguration? configuration; // A set of key/value application configuration properties.

        /// <summary>
        /// Gets or sets the path to the default workspace.
        /// </summary>
        public string Workspace
        {
            get { return GetStringValue($"{Constants.SETTINGS}:{Constants.WORKSPACE}"); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the specified <see cref="IViewConfiguration"/>.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IViewConfiguration"/>.</param>
        /// <returns>The specified <see cref="IViewConfiguration"/>.</returns>
        public IViewConfiguration GetViewConfiguration(string name)
        {
            return views[name]; // Return EmptyConfiguration and log missing view
        }

        /// <summary>
        /// Loads the application configuration.
        /// </summary>
        public void Initialise()
        {
            // TODO - This is not working very well - lots of errors on start up


            var builder = new ConfigurationBuilder();

            builder.AddXmlFile(Constants.CONFIGURATION);

            configuration = builder.Build();

            LoadConfiguredViews();
        }

        /// <summary>
        /// Gets the specified <see cref="string"/> value.
        /// </summary>
        /// <param name="key">The key that identifies the required string value.</param>
        /// <returns>The specified <see cref="string"/> value.</returns>
        private string GetStringValue(string key)
        {
            Debug.Assert(configuration != null);

            string? setting = configuration[key];

            if (string.IsNullOrEmpty(setting)) setting = string.Empty;

            return setting;
        }

        /// <summary>
        /// Loads the view configurations.
        /// </summary>
        private void LoadConfiguredViews()
        {
            Debug.Assert(configuration != null);

            var views = new Views();

            configuration.GetSection($"{Constants.SETTINGS}:{Constants.VIEWS}").Bind(views);

            foreach (var view in views)
            {
                var config = new ViewConfiguration(view);
                this.views.Add(config.Name, config);
            }
        }
    }
}
