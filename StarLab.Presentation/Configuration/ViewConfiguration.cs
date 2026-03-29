namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// The configuration for an <see cref="IView"/>.
    /// </summary>
    internal class ViewConfiguration : IViewConfiguration
    {
        private readonly Dictionary<string, IChildViewConfiguration> configurations = new Dictionary<string, IChildViewConfiguration>(); // A dictionary containing the child view configurations indexed by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewConfiguration"/> class.
        /// </summary>
        /// <param name="name">The name of the view configuration.</param>
        /// <param name="type">A <see cref="ViewTypes"/> enum that specifies the view type.</param>
        /// <exception cref="ArgumentException"></exception>
        public ViewConfiguration(string name, ViewTypes type)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            Name = name;
            Type = type;
        }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the view type.
        /// </summary>
        public ViewTypes Type { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IChildViewConfiguration}"/> containing the available child view configurations.
        /// </summary>
        public IEnumerable<IChildViewConfiguration> ChildConfigurations => configurations.Values;

        /// <summary>
        /// Adds the specified <see cref="IChildViewConfiguration">.
        /// </summary>
        /// <param name="name">The name of the child view configuration.</param>
        /// <param name="panel">A <see cref="SplitViewPanels"/> enum that specifies which panel will be used to display the child view.</param>
        /// <param name="view">The type name of the child view.</param>
        /// <param name="presenter">The type name of the child view presenter.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public ViewConfiguration AddChild(string name, SplitViewPanels panel, string view, string presenter)
        {
            configurations.Add(name, new ChildViewConfiguration(name, panel, view, presenter));

            return this;
        }

        /// <summary>
        /// Adds the specified <see cref="IChildViewConfiguration">.
        /// </summary>
        /// <param name="name">The name of the child view configuration.</param>
        /// <param name="view">The type name of the child view.</param>
        /// <param name="presenter">The type name of the child view presenter.</param>
        /// <returns>A reference to this instance that allows the calling code to be written in the fluent style.</returns>
        public ViewConfiguration AddChild(string name, string view, string presenter)
        {
            configurations.Add(name, new ChildViewConfiguration(name, view, presenter));

            return this;
        }

        /// <summary>
        /// Gets the specified <see cref="IChildViewConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IChildViewConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IChildViewConfiguration"/> instance.</returns>
        public IChildViewConfiguration GetChildViewConfiguration(string name)
        {
            return configurations[name];
        }
    }
}
