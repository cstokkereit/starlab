namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Represents the application configuration.
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Gets the available document definitions.
        /// </summary>
        IEnumerable<IDocumentDefinition> DocumentDefinitions { get; }

        /// <summary>
        /// Gets the names of the available chart overlays.
        /// </summary>
        IEnumerable<string> Overlays { get; }
    }
}
