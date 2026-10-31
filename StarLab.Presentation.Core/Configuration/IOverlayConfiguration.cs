namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Represents the configuration information required to display a chart overlay.
    /// </summary>
    public interface IOverlayConfiguration
    {
        /// <summary>
        /// The path to the file containing the overlay data.
        /// </summary>
        string DataSource { get; }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type name of the overlay.
        /// </summary>
        string TypeName { get; }
    }
}
