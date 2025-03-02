using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents the current state of a workspace.
    /// </summary>
    internal class Workspace : IFolder
    {
        private readonly Dictionary<string, Document> documents = new Dictionary<string, Document>(); // A dictionary containing all of the documents within the workspace hierarchy.

        private readonly Dictionary<string, IFolder> projects = new Dictionary<string, IFolder>(); // A dictionary containing all of the projects within the workspace hierarchy.

        private readonly Dictionary<string, IFolder> folders = new Dictionary<string, IFolder>(); // A dictionary containing all of the folders within the workspace hierarchy.

        private readonly string? layout;

        /// <summary>
        /// Initialises a new instance of the <see cref="Workspace"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Workspace"/>.</param>
        public Workspace(WorkspaceDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));
            
            CreateProjects(dto.Projects);

            layout = dto.Layout;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Workspace"/> class.
        /// </summary>
        public Workspace() { }

        /// <summary>
        /// Gets an <see cref="IEnumerable{Document}"/> containing the documents in the workspace hierarchy.
        /// </summary>
        public IEnumerable<Document> Documents => documents.Values;

        /// <summary>
        /// Gets an <see cref="IEnumerable{IFolder}"/> containing the folders in the workspace hierarchy.
        /// </summary>
        public IEnumerable<IFolder> Folders => folders.Values;

        /// <summary>
        /// Returns true if the workspace does not contain any projects; false otherwise.
        /// </summary>
        public bool IsEmpty => projects.Count == 0;

        /// <summary>
        /// Gets the workspace layout.
        /// </summary>
        public string? Layout => layout;

        /// <summary>
        /// Gets the workspace name.
        /// </summary>
        public string Name { get => Constants.WORKSPACE; set => throw new InvalidOperationException(); }

        /// <summary>
        /// Gets the workspace path.
        /// </summary>
        public string Path { get => Constants.WORKSPACE; }

        /// <summary>
        /// The <see cref="Workspace"/> is the top most element in the workspace hierarchy and therefore has no parent.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public IFolder Parent => throw new InvalidOperationException();

        /// <summary>
        /// Gets an <see cref="IEnumerable{IFolder}"/> containing the projects in the workspace hierarchy. 
        /// </summary>
        public IEnumerable<IFolder> Projects => projects.Values;

        /// <summary>
        /// Adds the <see cref="Document"/> provided to the workspace hierarchy.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be added.</param>
        public void AddDocument(Document document)
        {
            var folder = GetFolder(document.Path);
            folder.AddDocument(document);

            documents.Add(document.ID, document);
        }

        /// <summary>
        /// Adds a new folder at the specified location within the workspace hierarchy.
        /// </summary>
        /// <param name="name">The name of the new folder.</param>
        /// <param name="parent">The parent <see cref="IFolder"/>.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddFolder(string name, IFolder parent)
        {
            if (parent is Workspace) throw new InvalidOperationException();

            var folder = new Folder(name, parent);

            folders.Add(folder.Path, folder);
        }

        /// <summary>
        /// Adds the <see cref="IFolder"/> provided to the workspace hierarchy.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being added.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddFolder(IFolder folder)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Deletes the <see cref="Document"/> provided from the workspace hierarchy.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be deleted.</param>
        public void DeleteDocument(Document document)
        {
            if (document != null && folders.ContainsKey(document.Path))
            {
                var folder = folders[document.Path];

                if (folder != null)
                {
                    folder.DeleteDocument(document);
                    documents.Remove(document.ID);
                }
            }
        }

        /// <summary>
        /// Removes the specified document from the workspace hierarchy.
        /// </summary>
        /// <param name="id">The ID of the document to be deleted.</param>
        public void DeleteDocument(string id)
        {
            DeleteDocument(GetDocument(id));
        }

        /// <summary>
        /// Removes the folder provided from the workspace hierarchy.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being removed.</param>
        public void DeleteFolder(IFolder folder)
        {
            if (folder != null && folder.Parent != null)
            {
                DeleteFolders(folder);

                if (folder.Parent != this) folder.Parent.DeleteFolder(folder);
            }
        }

        /// <summary>
        /// Removes the specified folder from the workspace hierarchy.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        public void DeleteFolder(string path)
        {
            var folder = GetFolder(path);

            if (folder is Project)
            {
                projects.Remove(folder.Path);

                DeleteFolders(folder);
            }
            else
            {
                DeleteFolder(folder);
            }
        }

        /// <summary>
        /// Gets the specified <see cref="Document"/>.
        /// </summary>
        /// <param name="id">The ID of the required document.</param>
        /// <returns>The required <see cref="Document"/>.</returns>
        public Document GetDocument(string id)
        {
            return documents[id];
        }

        /// <summary>
        /// Gets the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="path">The path to the required folder.</param>
        /// <returns>The required <see cref="IFolder"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public IFolder GetFolder(string path)
        {
            if (path.Equals(Constants.WORKSPACE)) return this;
            
            if (projects.ContainsKey(path)) return projects[path];
            
            if (folders.ContainsKey(path)) return folders[path];
            
            throw new KeyNotFoundException(path);
        }

        /// <summary>
        /// Gets the specified <see cref="Project"/>.
        /// </summary>
        /// <param name="name">The name of the required project.</param>
        /// <returns>The required <see cref="Project"/>.</returns>
        public Project GetProject(string name)
        {
            return (Project)projects[$"{Constants.WORKSPACE}/{name}"];
        }

        /// <summary>
        /// Renames the <see cref="Document"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> being renamed.</param>
        /// <param name="name">The new name.</param>
        public void RenameDocument(Document document, string name)
        {
            document.Name = name;
        }

        /// <summary>
        /// Renames the <see cref="IFolder"/> provided.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being renamed.</param>
        /// <param name="name">The new name.</param>
        public void RenameFolder(IFolder folder, string name)
        {
            if (folder is Project)
            {
                folder.Name = name;
            }
            else
            {
                var project = GetProject(folder);
                project.RenameFolder(folder, name);
            }
            
            UpdateProjects();
        }

        /// <summary>
        /// Creates the projects defined in the <see cref="IEnumerable{ProjectDTO}"/> provided.
        /// </summary>
        /// <param name="dtos">An <see cref="IEnumerable{ProjectDTO}"/> that contains the definitions of the projects to be created.</param>
        private void CreateProjects(IEnumerable<ProjectDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var project = new Project(dto, this);

                projects.Add(project.Path, project);

                foreach (var folder in project.AllFolders)
                {
                    folders.Add(folder.Path, folder);
                }

                foreach (var document in project.AllDocuments)
                {
                    documents.Add(document.ID, document);
                }
            }
        }

        /// <summary>
        /// Removes all documents from the <see cref="IFolder"/> provided.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that contains the documents.</param>
        private void DeleteDocuments(IFolder folder)
        {
            foreach (var document in folder.Documents)
            {
                documents.Remove(document.ID);
            }
        }

        /// <summary>
        /// Removes all child folders from the <see cref="IFolder"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IFolder"/> that contains the child folders.</param>
        private void DeleteFolders(IFolder parent)
        {
            var folders = new List<IFolder>(this.folders.Values);

            foreach (var folder in folders)
            {
                if (folder.Path.StartsWith(parent.Path))
                {
                    this.folders.Remove(folder.Path);
                    DeleteDocuments(folder);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="Project"/> that contains the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="folder">An <see cref="IFolder"/> from the required <see cref="Project"/>.</param>
        /// <returns>The <see cref="Project"/> that contains the specified <see cref="IFolder"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        private Project GetProject(IFolder folder)
        {
            foreach(var project in Projects)
            {
                if (folder.Path.StartsWith(project.Path)) return (Project)project;
            }

            throw new ArgumentException(nameof(folder)); // TODO
        }

        /// <summary>
        /// Adds the folders from the <see cref="Project"/> provided to the dictionary containing all of the folders within the workspace hierarchy.
        /// </summary>
        /// <param name="project">The <see cref="Project"/> containing the folders to be added.</param>
        private void UpdateProject(Project project)
        {
            foreach (var folder in project.AllFolders)
            {
                folders.Add(folder.Path, folder);
            }
        }

        /// <summary>
        /// Adds the folders from all projects within the workspace hierarchy to the dictionary containing all of the folders within the workspace hierarchy.
        /// </summary>
        private void UpdateProjects()
        {
            folders.Clear();

            foreach (var project in projects.Values)
            {
                UpdateProject((Project)project);
            }
        }
    }
}
