using StarLab.Presentation;

namespace StarLab.UI
{
    /// <summary>
    /// A wrapper around the application settings defined in the App.config file.
    /// </summary>
    public class ApplicationSettings : IApplicationSettings
    {
        /// <summary>
        /// Gets or sets the path to the default workspace file.
        /// </summary>
        public string Workspace
        {
            get { return Properties.Settings.Default.Workspace; } 

            set
            {
                Properties.Settings.Default.Workspace = value; 
                Properties.Settings.Default.Save();
            }
        }
    }
}
