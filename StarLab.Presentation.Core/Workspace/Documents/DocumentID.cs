using StarLab.Application;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A strongly typed document ID.
    /// </summary>
    public class DocumentID : ID<IDocument>
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
