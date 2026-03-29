namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Represents the configuration for an <see cref="IView"/>.
    /// </summary>
    public interface IViewConfiguration
    {
        /// <summary>
        /// Gets the view name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the view type.
        /// </summary>
        ViewTypes Type { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IChildViewConfiguration}"/> containing the available child view configurations.
        /// </summary>
        IEnumerable<IChildViewConfiguration> ChildConfigurations { get; }

        /// <summary>
        /// Gets the specified <see cref="IChildViewConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IChildViewConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IChildViewConfiguration"/> instance.</returns>
        IChildViewConfiguration GetChildViewConfiguration(string name);
    }
}
