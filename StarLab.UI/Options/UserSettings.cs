using StarLab.Presentation.Options;

namespace StarLab.UI.Options
{
    /// <summary>
    /// Provides read/write access to the user settings.
    /// </summary>
    public class UserSettings : IUserSettings
    {
        /// <summary>
        /// Gets the chart settings.
        /// </summary>
        public IChartSettings Charts => new ChartSettings();

        /// <summary>
        /// Gets or sets the path to the default workspace file.
        /// </summary>
        public string Workspace
        {
            get => Properties.Settings.Default.Workspace;
            set => Properties.Settings.Default.Workspace = value;
        }

        /// <summary>
        /// Saves the current values of the user settings.
        /// </summary>
        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
