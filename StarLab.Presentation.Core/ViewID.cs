using StarLab.Application;
using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// A strongly typed view ID.
    /// </summary>
    public class ViewID : ID<View>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ViewID"/> class.
        /// </summary>
        /// <param name="id">The ID of the document that the view represents.</param>
        public ViewID(DocumentID id)
            : base(id.ToString()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewID"/> class.
        /// </summary>
        /// <param name="document">The document that the view represents.</param>
        public ViewID(IDocument document)
            : this(document.ID) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewID"/> class.
        /// </summary>
        /// <param name="value">The ID of the document that the view represents.</param>
        public ViewID(string value) 
            : base(value) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewID"/> class.
        /// </summary>
        public ViewID() 
            : base() { }
    }
}
