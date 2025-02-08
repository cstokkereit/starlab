using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents the state of a project within the workspace hierarchy.
    /// </summary>
    internal class Project : IFolder
    {
        private IFolder folder; // The project folder.

        /// <summary>
        /// Initialises a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Project"/>.</param>
        /// <param name="parent">The <see cref="IFolder"/> that contains the <see cref="Project"/></param>
        public Project(ProjectDTO dto, IFolder parent)
        {
            folder = new Folder(dto.Name, dto.Expanded, parent);
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
    }
}
