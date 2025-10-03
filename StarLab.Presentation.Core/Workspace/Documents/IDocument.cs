using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Represents a document within a workspace.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Gets the chart.
        /// </summary>
        IChart Chart { get; }

        /// <summary>
        /// Gets the document name including the path.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the document ID.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Gets the document name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the path to the folder that contains the document.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the name of the view config section.
        /// </summary>
        string View { get; }
    }
}
