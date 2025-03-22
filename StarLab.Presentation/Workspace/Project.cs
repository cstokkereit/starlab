using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Represents a project in the workspace hierarchy.
    /// </summary>
    internal class Project : IProject
    {
        private readonly List<IDocument> documents = new List<IDocument>(); // A list containing the documents that belong to this project.

        private readonly List<IFolder> folders = new List<IFolder>(); // A list containing the child folders.

        private readonly Folder folder; // The project folder.

        /// <summary>
        /// Initialises a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="ProjectDTO"/> representation of the project.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Project(ProjectDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException();

            folder = new Folder(dto.Name, dto.Expanded);

            CreateFolders(dto.Folders);
            CreateDocuments(dto.Documents);

            folder.AddChildFolders(folders);
        }

        /// <summary>
        /// Gets the documents that belong to this project.
        /// </summary>
        public IEnumerable<IDocument> Documents => documents;

        /// <summary>
        /// Returns true if the project is expanded; false otherwise.
        /// </summary>
        public bool Expanded { get; private set; }

        /// <summary>
        /// Gets the child folders.
        /// </summary>
        public IEnumerable<IFolder> Folders => folders;

        /// <summary>
        /// Gets the project key.
        /// </summary>
        public string Key => folder.Key;

        /// <summary>
        /// Gets the project name.
        /// </summary>
        public string Name => folder.Name;

        /// <summary>
        /// Gets the parent folder key.
        /// </summary>
        public string ParentKey => folder.ParentKey;

        /// <summary>
        /// Collapses the project.
        /// </summary>
        public void Collapse()
        {
            Expanded = false;
        }

        /// <summary>
        /// Recursively collapses the project and all of its children.
        /// </summary>
        public void CollapseAll()
        {
            foreach (var folder in folder.Folders)
            {
                folder.CollapseAll();
            }

            Collapse();
        }

        /// <summary>
        /// Expands the project.
        /// </summary>
        public void Expand()
        {
            Expanded = true;
        }

        /// <summary>
        /// Recursively expands the project and all of its children.
        /// </summary>
        public void ExpandAll()
        {
            foreach (var folder in folder.Folders)
            {
                folder.ExpandAll();
            }

            Expand();
        }

        /// <summary>
        /// Creates the folder hierarchy that belongs to this project from the <see cref="FolderDTO"/>s provided.
        /// </summary>
        /// <param name="dtos">An <see cref="IEnumerable{FolderDTO}"/> that contains the dtos.</param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                folders.Add(new Folder(dto));
            }

            foreach (var folder in folders)
            {
                if (folder is Folder parent) parent.AddChildFolders(folders);
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
                documents.Add(new Document(dto));
            }
        }
    }
}
