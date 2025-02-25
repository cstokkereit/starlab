using StarLab.Application.Workspace.Documents;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents the state of a project within the workspace hierarchy.
    /// </summary>
    internal class Project : IFolder
    {
        private readonly Dictionary<string, Folder> folders = new Dictionary<string, Folder>(); // A dictionary containing all of the folders within the project hierarchy.

        private IFolder folder; // The project folder.

        /// <summary>
        /// Initialises a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Project"/>.</param>
        /// <param name="parent">The <see cref="IFolder"/> that contains the <see cref="Project"/></param>
        public Project(ProjectDTO dto, IFolder parent)
        {
            ArgumentNullException.ThrowIfNull(parent, nameof(parent));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            folder = new Folder(dto.Name, dto.Expanded, parent);

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
        /// Gets an <see cref="IEnumerable{Document}"/> containing the documents in the project folder.
        /// </summary>
        public IEnumerable<Document> Documents => folder.Documents;

        /// <summary>
        /// Gets an <see cref="IEnumerable{IFolder}"/> containing the folders in the project folder.
        /// </summary>
        public IEnumerable<IFolder> Folders => folder.Folders;

        /// <summary>
        /// Returns true if the project does not contain any documents or folders; false otherwise.
        /// </summary>
        public bool IsEmpty => folder.IsEmpty;

        /// <summary>
        /// Gets the project name.
        /// </summary>
        public string Name { get => folder.Name; set => folder.Name = value; }

        /// <summary>
        /// Gets the <see cref="IFolder"/> that contains the project.
        /// </summary>
        public IFolder Parent => folder.Parent;

        /// <summary>
        /// Gets the project path.
        /// </summary>
        public string Path => folder.Path;

        /// <summary>
        /// Adds the <see cref="Document"/> provided to the project folder.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be added.</param>
        public void AddDocument(Document document)
        {
            folder.AddDocument(document);
        }

        /// <summary>
        /// Adds the <see cref="IFolder"/> provided to the project folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> to be added.</param>
        public void AddFolder(IFolder folder)
        {
            if (folder is not Folder) throw new InvalidOperationException(); // TODO

            this.folder.AddFolder(folder);
        }

        /// <summary>
        /// Deletes the <see cref="Document"/> provided from the folder.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be deleted.</param>
        public void DeleteDocument(Document document)
        {
            folder.DeleteDocument(document);
        }  

        /// <summary>
        /// Deletes the <see cref="IFolder"/> provided from the project folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> to be deleted.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeleteFolder(IFolder folder)
        {
            if (folder is not Folder) throw new InvalidOperationException(); // TODO

            this.folder.DeleteFolder(folder);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        public void RenameFolder(IFolder folder, string name)
        {
            if (folder is Project) throw new ArgumentException(); // TODO

            var children = GetChildFolders(folder);

            RemoveFolders(children);

            folders.Remove(folder.Name);

            folder.Name = name;

            folders.Add(folder.Name, (Folder)folder);

            AddFolders(children);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="folders"></param>
        private void AddFolders(IEnumerable<IFolder> folders)
        {
            foreach (var folder in folders)
            {
                this.folders.Add(folder.Name, (Folder)folder);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="folders"></param>
        private void RemoveFolders(IEnumerable<IFolder> folders)
        {
            foreach (var folder in folders)
            {
                this.folders.Remove(folder.Name);
            }
        }


        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private IEnumerable<IFolder> GetChildFolders(IFolder parent)
        {
            var children = new List<IFolder>();

            foreach (var folder in AllFolders)
            {
                if (folder.Parent == parent) children.Add(folder);
            }

            return children;
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
                    var folder = folders[dto.Path];

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
    }
}
