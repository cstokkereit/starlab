namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A strongly typed Document ID.
    /// </summary>
    internal class DocumentID  : ID<Document>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentID"/> class.
        /// </summary>
        /// <param name="value">The ID of the document.</param>
        public DocumentID(string value)
            : base(Guid.Parse(value)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentID"/> class.
        /// </summary>
        public DocumentID()
            : base() { }
    }
}
