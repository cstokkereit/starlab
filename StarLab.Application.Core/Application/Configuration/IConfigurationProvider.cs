namespace StarLab.Application.Configuration
{
    /// <summary>
    /// Represents a provider that can be used to obtain the application configuration.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets or sets the default workspace.
        /// </summary>
        string Workspace { get; set; }

        /// <summary>
        /// Gets the specified <see cref="IViewConfiguration"/>.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IViewConfiguration"/>.</param>
        /// <returns>The specified <see cref="IViewConfiguration"/>.</returns>
        IViewConfiguration GetViewConfiguration(string name);

        /// <summary>
        /// Loads the application configuration.
        /// </summary>
        void Initialise();
    }
}
