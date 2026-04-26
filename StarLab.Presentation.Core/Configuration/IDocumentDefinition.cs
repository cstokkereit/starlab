using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Represents a document definition. This interface is used to provide the information necessary for creating a document.
    /// </summary>
    public interface IDocumentDefinition
    {
        /// <summary>
        /// Gets a description of the document type.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the display name of the document type.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the name of the image that represents the document type.
        /// </summary>
        string Image { get; }

        /// <summary>
        /// Gets the name of the document type.
        /// </summary>
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        DocumentTypes Type { get; }

        /// <summary>
        /// Gets the name of the view that is used to display the document.
        /// </summary>
        string View { get; }
    }
}
