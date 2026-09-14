namespace StarLab.Application
{
    /// <summary>
    /// An exception that is thrown when a folder with the same name already exists at the specified location.
    /// </summary>
    public class FolderExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FolderExistsException"/> class.
        /// </summary>
        /// <param name="source">The source folder path.</param>
        /// <param name="destination">The destination folder path.</param>
        public FolderExistsException(string source, string destination)
        {
            DestinationFolder = destination;
            SourceFolder = source;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderExistsException"/> class.
        /// </summary>
        /// <param name="destination">The destination folder path.</param>
        public FolderExistsException(string destination)
            : this(string.Empty, destination) { }

        /// <summary>
        /// Gets the destination folder path.
        /// </summary>
        public string DestinationFolder { get; }

        /// <summary>
        /// Gets the source folder path.
        /// </summary>
        public string SourceFolder { get; }
    }
}
