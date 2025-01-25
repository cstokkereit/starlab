namespace StarLab.Application.Configuration
{
    /// <summary>
    /// Holds the configuration for a child view.
    /// </summary>
    internal readonly struct ChildViewConfiguration : IChildViewConfiguration
    {
        private readonly int panel; // The ID of the panel that will be used to display the child view.

        private readonly string name; // The child view name.

        private readonly string presenter; // The type name of the presenter that controls the child view.

        private readonly string view; // The type name of the child view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChildViewConfiguration"/> class.
        /// </summary>
        /// <param name="childView">The <see cref="ChildView"/> containing the configuration.</param>
        public ChildViewConfiguration(ChildView childView)
        {
            panel = string.IsNullOrEmpty(childView.Panel) ? 0 : int.Parse(childView.Panel);
            presenter = string.Empty + childView.Presenter;
            name = string.Empty + childView.Name;
            view = string.Empty + childView.View;
        }

        /// <summary>
        /// Gets the child view name.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the ID of the panel that will be used to display the child view.
        /// </summary>
        public int Panel => panel;

        /// <summary>
        /// Gets the type name of the presenter that controls the child view.
        /// </summary>
        public string Presenter => presenter;

        /// <summary>
        /// Gets the type name of the child view.
        /// </summary>
        public string View => view;
    }
}
