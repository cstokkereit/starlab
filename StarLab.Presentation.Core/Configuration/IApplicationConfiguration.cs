namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// TODO : Documentation
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{IDocumentDefinition}"/> that contains the available document definitions.
        /// </summary>
        IEnumerable<IDocumentDefinition> DocumentDefinitions { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{string}"/> that contains the names of the available chart overlays.
        /// </summary>
        IEnumerable<string> Overlays { get; }
    }
}
