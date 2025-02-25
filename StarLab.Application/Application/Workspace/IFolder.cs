using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a folder in the workspace hierarchy.
    /// </summary>
    internal interface IFolder
    {
        /// <summary>
        /// Gets an <see cref="IEnumerable{Document}"/> containing the documents contained in the folder.
        /// </summary>
        IEnumerable<Document> Documents { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IFolder}"/> containing the folders that are children of the folder.
        /// </summary>
        IEnumerable<IFolder> Folders { get; }

        /// <summary>
        /// Returns true if the folder does not contain any documents or folders; false otherwise.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the parent <see cref="IFolder"/>.
        /// </summary>
        IFolder Parent { get; }

        /// <summary>
        /// Gets the path to the folder.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Adds the <see cref="Document"/> provided to the folder.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> being added.</param>
        void AddDocument(Document document);

        /// <summary>
        /// Adds the <see cref="IFolder"/> provided to the folder.
        /// </summary>
        /// <param name="document">The <see cref="IFolder"/> being added.</param>
        void AddFolder(IFolder folder);

        /// <summary>
        /// Deletes the <see cref="Document"/> provided from the folder.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be deleted.</param>
        void DeleteDocument(Document document);

        /// <summary>
        /// Removes the <see cref="IFolder"/> provided from the folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being removed.</param>
        void DeleteFolder(IFolder folder);
    }
}
