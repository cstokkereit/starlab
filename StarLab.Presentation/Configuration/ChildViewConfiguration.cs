namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// The configuration for an <see cref="IChildView"/>.
    /// </summary>
    internal class ChildViewConfiguration : IChildViewConfiguration
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChildViewConfiguration"/> class.
        /// </summary>
        /// <param name="name">The name of the view configuration.</param>
        /// <param name="panel">A <see cref="SplitViewPanels"/> enum that specifies which panel will be used to display the child view.</param>
        /// <param name="view">The type name of the child view.</param>
        /// <param name="presenter">The type name of the child view presenter.</param>
        /// <exception cref="ArgumentException"></exception>
        public ChildViewConfiguration(string name, SplitViewPanels panel, string view, string presenter)
        {
            ArgumentException.ThrowIfNullOrEmpty(presenter, nameof(presenter));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(view, nameof(view));

            Presenter = presenter;
            Panel = panel;
            Name = name;
            View = view;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChildViewConfiguration"/> class.
        /// </summary>
        /// <param name="name">The name of the view configuration.</param>
        /// <param name="view">The type name of the child view.</param>
        /// <param name="presenter">The type name of the child view presenter.</param>
        public ChildViewConfiguration(string name, string view, string presenter)
            : this(name, SplitViewPanels.Any, view, presenter) { }

        /// <summary>
        /// Gets the child view name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the <see cref="SplitViewPanels"/> that specifies which panel will be used to display the child view.
        /// </summary>
        public SplitViewPanels Panel { get; }

        /// <summary>
        /// Gets the type name of the presenter that controls the child view.
        /// </summary>
        public string Presenter { get; }

        /// <summary>
        /// Gets the type name of the child view.
        /// </summary>
        public string View { get; }
    }
}
