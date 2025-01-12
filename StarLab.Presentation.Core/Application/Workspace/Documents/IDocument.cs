namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Represents a document within a workspace.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// An event that will be raised whenever the document name is changed.
        /// </summary>
        public event EventHandler<string>? NameChanged;

        /// <summary>
        /// Gets the document name including the path.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the document ID.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Gets or sets the document name.
        /// </summary>
        string Name { get; set; }

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
