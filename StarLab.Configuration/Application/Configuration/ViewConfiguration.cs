namespace StarLab.Application.Configuration
{
    /// <summary>
    /// Holds the configuration for a view.
    /// </summary>
    internal struct ViewConfiguration : IViewConfiguration
    {
        private readonly Dictionary<string, IChildViewConfiguration> childViewsByName = new Dictionary<string, IChildViewConfiguration>(); // A dictionary containing the child view configurations indexed by name.

        private readonly List<IChildViewConfiguration> childViews = new List<IChildViewConfiguration>(); // A list containing the child view configurations.

        private readonly ViewTypes type; // The view type.

        private readonly string name; // The view name.

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewConfiguration"/> class.
        /// </summary>
        /// <param name="view">The <see cref="View"/> containing the configuration.</param>
        /// <exception cref="ArgumentException"></exception>
        public ViewConfiguration(View view)
        {
            name = view.Name ?? throw new ArgumentException(nameof(view));

            switch (view.Type)
            {
                case Constants.APPLICATION:
                    type = ViewTypes.Application;
                    break;

                case Constants.DIALOG:
                    type = ViewTypes.Dialog;
                    break;

                case Constants.DOCUMENT:
                    type = ViewTypes.Document;
                    break;

                case Constants.TOOL:
                    type = ViewTypes.Tool;
                    break;

                default:
                    throw new ArgumentException(); // TODO
            }

            LoadChildViews(view);
        }

        /// <summary>
        /// Gets an <see cref="IList{IChildViewConfiguration}"/> containing the configuration for the child views.
        /// </summary>
        public IList<IChildViewConfiguration> ChildViews => childViews;

        /// <summary>
        /// Gets the view name.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the view type.
        /// </summary>
        public ViewTypes Type => type;

        /// <summary>
        /// Gets the <see cref="IChildViewConfiguration"/> containing the configuration for the specified child view.
        /// </summary>
        /// <param name="name">The name of the child view.</param>
        /// <returns>The <see cref="IChildViewConfiguration"/> for the specified child view.</returns>
        public IChildViewConfiguration GetChildViewConfiguration(string name) => childViewsByName[name];

        /// <summary>
        /// Loads the child view configurations from the <see cref="View"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="View"/> containing the child view configurations being loaded.</param>
        private void LoadChildViews(View view)
        {
            if (view.ChildView != null)
            {
                childViews.Add(new ChildViewConfiguration(view.ChildView));
            }
            else if (view.ChildViews != null)
            {
                foreach (var childView in view.ChildViews)
                {
                    var configuration = new ChildViewConfiguration(childView);
                    childViewsByName.Add(configuration.Name, configuration);
                    childViews.Add(configuration);
                }
            }
        }
    }
}
