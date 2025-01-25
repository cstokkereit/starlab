namespace StarLab.Application.Configuration
{
    /// <summary>
    /// Represents the configuration for a child view.
    /// </summary>
    public interface IChildViewConfiguration
    {
        /// <summary>
        /// Gets the ID of the panel that will be used to display the child view.
        /// </summary>
        int Panel { get; }

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
