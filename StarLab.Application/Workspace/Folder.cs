using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Domain model represention of a folder.
    /// </summary>
    internal class Folder : IFolder
    {
        private readonly List<Document> documents = new List<Document>(); // A list containing the documents contained within the folder.

        private readonly List<IFolder> folders = new List<IFolder>(); // A list containing the folders contained within the folder.

        private readonly bool expanded; // A flag indicating that the folder has been expanded.

        /// <summary>
        /// Initialises a new instance of the <see cref="Folder"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the folder.</param>
        /// <param name="parent">The <see cref="IFolder"/> that contains the folder.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public Folder(FolderDTO dto, IFolder parent)
        {
            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentException(Constants.InvalidPathMessage);

            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Name = dto.Path.Substring(dto.Path.LastIndexOf('/') + 1);
            
            expanded = dto.Expanded;

            Parent.AddFolder(this);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Folder"/> class.
        /// </summary>
        /// <param name="name">The name of the folder.</param>
        /// <param name="expanded">true if the folder is expanded; false otherwise.</param>
        /// <param name="parent">The <see cref="IFolder"/> that contains the folder.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Folder(string? name, bool expanded, IFolder parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            this.expanded = expanded;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Folder"/> class.
        /// </summary>
        /// <param name="name">The name of the folder.</param>
        /// <param name="parent">The <see cref="IFolder"/> that contains the folder.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Folder(string name, IFolder parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            Parent.AddFolder(this);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{Document}"/> containing the documents in the folder.
        /// </summary>
        public IEnumerable<Document> Documents => documents;

        /// <summary>
        /// Returns true if the folder is expanded; false otherwise.
        /// </summary>
        public bool Expanded => expanded;

        /// <summary>
        /// Gets an <see cref="IEnumerable{IFolder}"/> containing the folders in the folder.
        /// </summary>
        public IEnumerable<IFolder> Folders => folders;

        /// <summary>
        /// Returns true if the folder does not contain any documents or folders; false otherwise.
        /// </summary>
        public bool IsEmpty => documents.Count == 0 && folders.Count == 0;

        /// <summary>
        /// Gets or sets the folder name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        public string Path
        {
            get { return $"{Parent.Path}/{Name}"; }
        }

        /// <summary>
        /// Gets the <see cref="IFolder"/> that contains the folder.
        /// </summary>
        public IFolder Parent { get; private set; }

        /// <summary>
        /// Adds the <see cref="Document"/> provided to the folder.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be added.</param>
        public void AddDocument(Document document)
        {
            documents.Add(document);
        }

        /// <summary>
        /// Adds the <see cref="IFolder"/> provided to the folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> to be added.</param>
        public void AddFolder(IFolder folder)
        {
            if (folder.Parent != this && folder is Folder child) child.Parent = this;

            folders.Add(folder);
        }

        /// <summary>
        /// Determines if this <see cref="IFolder"> contains a child folder with the specified name.
        /// </summary>
        /// <param name="name">The name of the child folder.</param>
        /// <returns><see cref="true"/> if this folder contains a child folder with the specified name; <see cref="false"/> otherwise.</returns>
        public bool ContainsFolder(string name)
        {
            foreach (IFolder child in folders)
            {
                if (child.Name == name) return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the <see cref="Document"/> provided from the folder.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be deleted.</param>
        public void DeleteDocument(Document document)
        {
            documents.Remove(document);
        }

        /// <summary>
        /// Deletes the <see cref="IFolder"/> provided from the folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> to be deleted.</param>
        public void DeleteFolder(IFolder folder)
        {
            folders.Remove(folder);
        }
    }
}
