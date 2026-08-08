namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Provides context for the <see cref="IView"/> being displayed.
    /// </summary>
    public readonly struct AddDocumentViewContext : IViewContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentViewContext"> class.
        /// </summary>
        /// <param name="path">A string that specifies the path to the folder within the workspace hierarchy that will contain the new document.</param>
        /// <param name="type">An <see cref="DocumentTypes"/> that specifies the type of document to add.</param>
        public AddDocumentViewContext(string path, DocumentTypes type)
        {
            Path = path;
            Type = type;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentViewContext"> class.
        /// </summary>
        /// <param name="path">A string that specifies the path to the folder within the workspace hierarchy that will contain the new document.</param>
        public AddDocumentViewContext(string path)
            : this(path, DocumentTypes.Any) { }

        /// <summary>
        /// Gets the path to  the folder.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public DocumentTypes Type { get; }
    }
}
