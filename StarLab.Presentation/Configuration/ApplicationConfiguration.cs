using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Provides access to the application configuration.
    /// </summary>
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private readonly List<IDocumentDefinition> documentDefinitions = new List<IDocumentDefinition>(); // A list containing the available document definitions.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationConfiguration"/> class.
        /// </summary>
        public ApplicationConfiguration()
        {
            LoadDocumentTypes();
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IDocumentDefinition}"/> that contains the available document definitions.
        /// </summary>
        public IEnumerable<IDocumentDefinition> DocumentDefinitions => documentDefinitions;

        /// <summary>
        /// Loads the available document definitions.
        /// </summary>
        private void LoadDocumentTypes()
        {
            documentDefinitions.Add(new DocumentDefinition("ColourMagnitudeDiagram", "Colour-Magnitude Diagram", "", DocumentTypes.Chart, "ColourMagnitudeDiagram32X32", "ColourMagnitudeDiagramView"));
            documentDefinitions.Add(new DocumentDefinition("ColourColourDiagram", "Colour-Colour Diagram", "", DocumentTypes.Chart, "ColourColourDiagram32X32", "ColourColourDiagramView"));
        }
    }
}
