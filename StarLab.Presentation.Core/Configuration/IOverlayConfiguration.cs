namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// TODO : Documentation
    /// </summary>
    public interface IOverlayConfiguration
    {
        /// <summary>
        /// TTODO
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
