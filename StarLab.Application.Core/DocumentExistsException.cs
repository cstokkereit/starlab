namespace StarLab.Application
{
    /// <summary>
    /// An exception that is thrown when a document with the same name already exists at the specified location.
    /// </summary>
    public class DocumentExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentExistsException"/> class.
        /// </summary>
        /// <param name="name">The document ID.</param>
        /// <param name="name">The document name.</param>
        /// <param name="path">The document path.</param>
        public DocumentExistsException(object id, string name, string path)
        {
            ID = $"{id?.ToString()}";

            Name = name;
            Path = path;
        }

        /// <summary>
        /// Gets the document ID.
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Gets the document name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the document path.
        /// </summary>
        public string Path { get; }
    }
}
