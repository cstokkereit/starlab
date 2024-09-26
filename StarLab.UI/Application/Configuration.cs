using StarLab.Properties;

namespace StarLab.Application
{
    /// <summary>
    /// Stores the current configuration state. Implements the <see cref="IConfiguration"/> interface.
    /// </summary>
    public class Configuration : IConfiguration
    {
        #region IConfiguration Members

        /// <summary>
        /// A flag indicating that the configuration has been changed and needs to be saved.
        /// </summary>
        public bool Dirty { get; private set; }

        /// <summary>
        /// Gets or sets the absolute path to the default workspace.
        /// </summary>
        public string Workspace
        {
            get
            {
                return Settings.Default.Workspace;
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && value != Settings.Default.Workspace)
                {
                    Settings.Default.Workspace = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Saves the current configuration.
        /// </summary>
        public void Save()
        {
            if (Dirty)
            {
                Settings.Default.Save();
                Dirty = false;
            }
        }

        #endregion
    }
}
