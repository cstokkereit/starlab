using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Application model represention of a project.
    /// </summary>
    internal class Project : Folder
    {
        private readonly Dictionary<string, Folder> folders = new Dictionary<string, Folder>(); // A dictionary containing all of the folders within the project hierarchy.

        private readonly Database database; // The database associated with the project.

        /// <summary>
        /// Initialises a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Project"/>.</param>
        /// <param name="parent">The <see cref="IFolder"/> that contains the <see cref="Project"/></param>
        public Project(ProjectDTO dto, IFolder parent)
            : base(dto.Name, dto.Expanded,parent)
        {
            ArgumentNullException.ThrowIfNull(parent, nameof(parent));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            database = new Database(dto.Database);

            CreateFolders(dto.Folders);
            CreateDocuments(dto.Documents);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{Document}"/> containing all of the documents within the project hierarchy.
        /// </summary>
        public IEnumerable<Document> AllDocuments 
        {
            get
            {
                var documents = new List<Document>();

                GetDocuments(documents);

                foreach (var folder in Folders)
                {
                    GetDocuments(folder, documents);
                }

                return documents;
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IFolder}"/> containing all of the folders within the project hierarchy.
        /// </summary>
        public IEnumerable<IFolder> AllFolders 
        { 
            get
            {
                var folders = new List<IFolder>();

                foreach (var folder in Folders)
                {
                    GetFolders(folder, folders);
                }
                
                return folders;
            }
        }

        /// <summary>
        /// Gets the <see cref="Database"/> associated with the project.
        /// </summary>
        public Database Database => database;

        /// <summary>
        /// Renames the <see cref="IFolder"/> provided.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> to be renamed.</param>
        /// <param name="name">The new folder name.</param>
        public void RenameFolder(IFolder folder, string name)
        {
            //if (folder is Project) throw new ArgumentException(ExceptionMessages.UnexpectedArgumentType(typeof(Project), folder.GetType()), nameof(folder));

            var project = folder.Path == Path;

            var children = GetChildFolders(folder);

            RemoveFolders(children);

            if (!project)
            {
                folders.Remove(folder.Name);

                folder.Name = name;

                folders.Add(folder.Name, (Folder)folder);
            }
            else
            {
                folder.Name = name;
            }
            
            AddFolders(children);
        }

        /// <summary>
        /// Adds the folders provided to the dictionary containing all folders within the project hierarchy.
        /// </summary>
        /// <param name="folders">An <see cref="IEnumerable{IFolder}"/> containing the folders to be added.</param>
        private void AddFolders(IEnumerable<IFolder> folders)
        {
            foreach (var folder in folders)
            {
                this.folders.Add(folder.Path, (Folder)folder);
            }
        }

        /// <summary>
        /// Creates the documents that belong to this project from the <see cref="DocumentDTO"/>s provided.
        /// </summary>
        /// <param name="dtos">An <see cref="IEnumerable{DocumentDTO}"/> that contains the dtos.</param>
        private void CreateDocuments(IEnumerable<DocumentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Path))
                {
                    IFolder folder = this;

                    if (dto.Path != Path) folder = folders[dto.Path];

                    folder.AddDocument(new Document(dto, folder));
                }
            }
        }

        /// <summary>
        /// Creates the folder hierarchy that belongs to this project from the <see cref="FolderDTO"/>s provided.
        /// </summary>
        /// <param name="dtos">An <see cref="IEnumerable{FolderDTO}"/> that contains the dtos.</param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos)
        {
            Folder folder;

            foreach (var dto in dtos.OrderBy(folder => folder.Path))
            {
                if (!string.IsNullOrEmpty(dto.Path))
                {
                    var parentPath = dto.Path.Substring(0, dto.Path.LastIndexOf('/'));

                    if (parentPath == Path)
                    {
                        folder = new Folder(dto, this);
                    }
                    else
                    {
                        folder = new Folder(dto, folders[parentPath]);
                    }

                    folders.Add(folder.Path, folder);
                }    
            }
        }

        /// <summary>
        /// Gets all folders that are children of the <see cref="IFolder"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IFolder"/> that contains the required child folders.</param>
        /// <returns>An <see cref="IEnumerable{IFolder}"/> containing the child folders.</returns>
        private IEnumerable<IFolder> GetChildFolders(IFolder parent)
        {
            var children = new List<IFolder>();
            GetFolders(parent, children);
            return children;
        }

        /// <summary>
        /// A recursive method that collects all of the documents within the project hierarchy.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> containing the documents to be collected.</param>
        /// <param name="documents">A <see cref="List{Document}"/> containing the documents that have been collected.</param>
        private void GetDocuments(IFolder folder, List<Document> documents)
        {
            foreach (Document document in folder.Documents)
            {
                documents.Add(document);
            }

            foreach (var child in folder.Folders)
            {
                GetDocuments(child, documents);
            }
        }

        /// <summary>
        /// A method that collects the documents that are within the project folder.
        /// </summary>
        /// <param name="documents">A <see cref="List{Document}"/> containing the documents that have been collected.</param>
        private void GetDocuments(List<Document> documents)
        {
            foreach (Document document in Documents)
            {
                documents.Add(document);
            }
        }

        /// <summary>
        /// A recursive method that collects all of the folders within the project hierarchy.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> containing the folders to be collected.</param>
        /// <param name="folders">A <see cref="List{IFolder}"/> containing the folders that have been collected.</param>
        private void GetFolders(IFolder folder, List<IFolder> folders)
        {
            folders.Add(folder);

            foreach (var child in folder.Folders)
            {
                GetFolders(child, folders);
            }
        }

        /// <summary>
        /// Removes the folders provided from the dictionary containing all folders within the project hierarchy.
        /// </summary>
        /// <param name="folders">An <see cref="IEnumerable{IFolder}"/> containing the folders to be removed.</param>
        private void RemoveFolders(IEnumerable<IFolder> folders)
        {
            foreach (var folder in folders)
            {
                this.folders.Remove(folder.Path);
            }
        }
    }
}
