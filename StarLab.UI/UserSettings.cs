using StarLab.Presentation;

namespace StarLab.UI
{
    /// <summary>
    /// Provides read/write access to the user settings defined in the app.config file.
    /// </summary>
    public class UserSettings : IUserSettings
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
