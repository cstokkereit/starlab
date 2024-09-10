namespace StarLab.Presentation
{
    /// <summary>
    /// This interface represents the configuration state.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// A flag indicating that the configuration has been changed and needs to be saved.
        /// </summary>
        bool Dirty { get; }

        /// <summary>
        /// Gets or sets the absolute path to the default workspace.
        /// </summary>
        string Workspace { get; set; }

        /// <summary>
        /// Saves the current configuration.
        /// </summary>
        void Save();
    }
}
