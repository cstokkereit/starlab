namespace StarLab.Presentation.Options
{
    /// <summary>
    /// Represents the user settings defined in the app.Config file.
    /// </summary>
    public interface IUserSettings
    {
        /// <summary>
        /// Gets the chart settings.
        /// </summary>
        IChartSettings Charts { get; }

        /// <summary>
        /// Gets or sets the path to the default workspace file.
        /// </summary>
        string Workspace { get; set; }

        /// <summary>
        /// Saves the current values of the user settings.
        /// </summary>
        void Save();
    }
}
