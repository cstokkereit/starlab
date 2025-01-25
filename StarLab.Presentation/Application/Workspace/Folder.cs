namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a folder in the workspace hierarchy.
    /// </summary>
    internal class Folder : IFolder
    {
        private readonly List<IFolder> folders = new List<IFolder>(); // A list containing the child folders.

        private readonly string parentKey; // The key of the parent folder.

        private readonly string name; // The folder name.

        private readonly string key; // The folder key.

        /// <summary>
        /// Initialises a new instance of the <see cref="Folder"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="FolderDTO"/> representation of the folder.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Folder(FolderDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentException(); // TODO

            key = dto.Path;

            var index = key.LastIndexOf('/');
            parentKey = key.Substring(0, index);
            name = key.Substring(index + 1);

            Expanded = dto.Expanded;
            IsNew = dto.IsNew;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Folder"/> class.
        /// </summary>
        /// <param name="name">The name of the folder.</param>
        /// <param name="expanded"><see cref="true"/> if the folder is expanded; <see cref="false"/> otherwise.</param>
        /// <exception cref="ArgumentException"></exception>
        public Folder(string name, bool expanded)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            this.name = name;

            parentKey = Constants.WORKSPACE;
            key = $"{parentKey}/{name}";
            
            Expanded = expanded;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the folder is expanded; <see cref="false"/> otherwise.
        /// </summary>
        public bool Expanded { get; private set; }

        /// <summary>
        /// Gets the child folders.
        /// </summary>
        public IEnumerable<IFolder> Folders => folders;

        /// <summary>
        /// TODO
        /// </summary>
        public bool IsNew { get; private set; }

        /// <summary>
        /// Gets the folder key. 
        /// </summary>
        public string Key => key;

        /// <summary>
        /// Gets the name of the folder.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the parent folder key.
        /// </summary>
        public string ParentKey => parentKey;

        /// <summary>
        /// Collapses the folder.
        /// </summary>
        public void Collapse()
        {
            Expanded = false;
        }

        /// <summary>
        /// Recursively collapses the folder and all of its children.
        /// </summary>
        public void CollapseAll()
        {
            foreach (var folder in folders)
            {
                folder.CollapseAll();
            }

            Collapse();
        }

        /// <summary>
        /// Expands the folder.
        /// </summary>
        public void Expand()
        {
            Expanded = true;
        }

        /// <summary>
        /// Recursively expands the folder and all of its children.
        /// </summary>
        public void ExpandAll()
        {
            foreach (var folder in folders)
            {
                folder.ExpandAll();
            }

            Expand();
        }

        /// <summary>
        /// Adds the <see cref="IFolder"/>s that are children of this folder to the list of child folders.
        /// </summary>
        /// <param name="folders">An <see cref="IEnumerable{IFolder}"/> that contains the folders.</param>
        public void AddChildFolders(IEnumerable<IFolder> folders)
        {
            foreach (var folder in folders)
            {
                if (folder.ParentKey == key) this.folders.Add(folder);
            }
        }
    }
}
