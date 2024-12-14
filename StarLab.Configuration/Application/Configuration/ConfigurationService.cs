using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace StarLab.Application.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly Dictionary<string, IViewConfiguration> views = new Dictionary<string, IViewConfiguration>();

        private IConfiguration? configuration;

        public string Workspace
        {
            get { return GetStringValue("AppSettings:workspace"); }
            set { throw new NotImplementedException(); }
        }

        public IViewConfiguration GetViewConfiguration(string name)
        {
            return views[name]; // Return EmptyConfiguration and log missing view
        }

        public void Initialise()
        {
            var builder = new ConfigurationBuilder();

            builder.AddXmlFile("app.config");

            configuration = builder.Build();

            LoadConfiguredViews();
        }

        private string GetStringValue(string key)
        {
            Debug.Assert(configuration != null);

            string? setting = configuration[key];

            if (string.IsNullOrEmpty(setting)) setting = string.Empty;

            return setting;
        }

        private void LoadConfiguredViews()
        {
            Debug.Assert(configuration != null);

            var views = new Views();

            configuration.GetSection("AppSettings:views").Bind(views);

            foreach (var view in views)
            {
                var config = new ViewConfiguration(view);
                this.views.Add(config.Name, config);
            }
        }
    }
}
