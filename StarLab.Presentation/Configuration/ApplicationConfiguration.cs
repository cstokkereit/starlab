using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Provides access to the application configuration.
    /// </summary>
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private readonly List<IDocumentDefinition> documentDefinitions = new List<IDocumentDefinition>(); // A list containing the available document definitions.

        private readonly List<string> overlays = new List<string>(); // A list containing the names of the available chart overlays.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">An <see cref="IFactoryConfiguration"/> that includes the names of the available overlays.</param>
        public ApplicationConfiguration(IFactoryConfiguration configuration)
        {
            overlays.AddRange(configuration.Overlays);

            LoadDocumentTypes();
        }

        /// <summary>
        /// Gets the available document definitions.
        /// </summary>
        public IEnumerable<IDocumentDefinition> DocumentDefinitions => documentDefinitions;

        /// <summary>
        /// Gets the names of the available chart overlays.
        /// </summary>
        public IEnumerable<string> Overlays => overlays;

        /// <summary>
        /// Loads the available document definitions.
        /// </summary>
        private void LoadDocumentTypes()
        {
            documentDefinitions.Add(new DocumentDefinition("ColourMagnitudeDiagram", "Colour-Magnitude Diagram", "", DocumentTypes.Chart, "ColourMagnitudeDiagram32X32", "ColourMagnitudeDiagram"));
            documentDefinitions.Add(new DocumentDefinition("ColourColourDiagram", "Colour-Colour Diagram", "", DocumentTypes.Chart, "ColourColourDiagram32X32", "ColourColourDiagram"));
            documentDefinitions.Add(new DocumentDefinition("AstrometricDataTable", "Astrometric Data Table", "", DocumentTypes.Table, "Table32X32", "Table"));
            documentDefinitions.Add(new DocumentDefinition("PhotometricDataTable", "Photometric Data Table", "", DocumentTypes.Table, "Table32X32", "Table"));
            documentDefinitions.Add(new DocumentDefinition("SpectroscopicDataTable", "Spectroscopic Data Table", "", DocumentTypes.Table, "Table32X32", "Table"));
            documentDefinitions.Add(new DocumentDefinition("CustomTable", "Custom Table", "", DocumentTypes.Table, "Table32X32", "Table"));
        }
    }
}
