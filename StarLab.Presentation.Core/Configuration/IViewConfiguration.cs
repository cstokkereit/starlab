namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Represents the configuration for a view.
    /// </summary>
    public interface IViewConfiguration
    {
        /// <summary>
        /// Gets an <see cref="IList{IChildViewConfiguration}"/> containing the configuration for the child views.
        /// </summary>
        IList<IChildViewConfiguration> ChildViews { get; }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the view type.
        /// </summary>
        ViewTypes Type { get; }

        /// <summary>
        /// Gets the <see cref="IChildViewConfiguration"/> containing the configuration for the specified child view.
        /// </summary>
        /// <param name="name">The name of the child view.</param>
        /// <returns>The <see cref="IChildViewConfiguration"/> for the specified child view.</returns>
        IChildViewConfiguration GetChildViewConfiguration(string name);
    }
}
