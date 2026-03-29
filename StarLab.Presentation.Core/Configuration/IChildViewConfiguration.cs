namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Represents the configuration for an <see cref="IChildView"/>.
    /// </summary>
    public interface IChildViewConfiguration
    {
        /// <summary>
        /// Gets the child view name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="SplitViewPanels"/> that specifies which panel will be used to display the child view.
        /// </summary>
        SplitViewPanels Panel { get; }

        /// <summary>
        /// Gets the type name of the presenter that controls the child view.
        /// </summary>
        string Presenter { get; }

        /// <summary>
        /// Gets the type name of the child view.
        /// </summary>
        string View { get; }
    }
}
