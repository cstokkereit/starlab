namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Provides context for the add document workflow.
    /// </summary>
    public readonly struct AddDocumentWorkflowContext : IWorkflowContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentWorkflowContext"> class.
        /// </summary>
        /// <param name="path">A string that specifies the path to the folder within the workspace hierarchy that will contain the new document.</param>
        /// <param name="type">An <see cref="DocumentTypes"/> that specifies the type of document to add.</param>
        public AddDocumentWorkflowContext(string path, DocumentTypes type)
        {
            Path = path;
            Type = type;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentWorkflowContext"> class.
        /// </summary>
        /// <param name="path">A string that specifies the path to the folder within the workspace hierarchy that will contain the new document.</param>
        public AddDocumentWorkflowContext(string path)
        {
            Path = path;
        }

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
