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

        private readonly string? layout; // Holds the position, size and state of the dockable views.

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
        public string Name { get => Constants.Workspace; set => throw new InvalidOperationException(); }

        /// <summary>
        /// Gets the workspace path.
        /// </summary>
        public string Path { get => Constants.Workspace; }

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
        /// <exception cref="NameExistsException"></exception>
        public void AddDocument(Document document)
        {
            if (DocumentExists(document)) throw new NameExistsException(ItemTypes.Document, document.Name);

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
        public Folder AddFolder(string name, IFolder parent)
        {
            var folder = new Folder(name, parent);
            
            AddFolder(folder);

            return folder;
        }

        /// <summary>
        /// Adds the <see cref="IFolder"/> provided at the specified location within the workspace hierarchy.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being added.</param>
        /// <param name="path">The path to the parent folder.</param>
        public void AddFolder(IFolder folder, string path)
        {
            if (folders.ContainsKey($"{path}/{folder.Name}")) throw new NameExistsException(ItemTypes.Folder, folder.Name);

            var parent = GetFolder(path);

            if (parent != null && folder is Folder child)
            {
                parent.AddFolder(child);

                folders.Add(folder.Path, folder);
            }
        }

        /// <summary>
        /// Adds the <see cref="IFolder"/> provided to the workspace hierarchy.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being added.</param>
        /// <exception cref="ArgumentException"></exception>
        public void AddFolder(IFolder folder)
        {
            if (folder is Workspace) throw new ArgumentException(); // TODO - Exception message

            if (folder is Project)
            {
                AddProject((Project)folder);
            }
            else
            {
                folders.Add(folder.Path, folder);
            }
        }

        /// <summary>
        /// Adds the <see cref="Project"/> provided to the workspace.
        /// </summary>
        /// <param name="project">The <see cref="Project"/> being added.</param>
        /// <exception cref="NameExistsException"></exception>
        public void AddProject(Project project)
        {
            if (projects.ContainsKey(project.Path)) throw new NameExistsException(ItemTypes.Project, project.Name);

            projects.Add(project.Path, project);
        }

        /// <summary>
        /// Determines if this <see cref="IFolder"> contains a child folder with the specified name.
        /// </summary>
        /// <param name="name">The name of the child folder.</param>
        /// <returns><see cref="true"/> if this folder contains a child folder with the specified name; <see cref="false"/> otherwise.</returns>
        public bool ContainsFolder(string name)
        {
            return false;
        }

        /// <summary>
        /// Deletes the <see cref="Document"/> provided from the workspace hierarchy.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> to be deleted.</param>
        public void DeleteDocument(Document document)
        {
            if (document != null)
            {
                IFolder folder;

                if (IsProject(document.Path))
                {
                    folder = projects[document.Path];
                }
                else if (IsFolder(document.Path))
                {
                    folder = folders[document.Path]; 
                }
                else
                {
                    throw new Exception(); // TODO - Exception message.
                }

                folder.DeleteDocument(document);

                documents.Remove(document.ID);
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
        /// Removes the folder provided along with any child folders and their contents from the workspace hierarchy.
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
        public IFolder GetFolder(string? path)
        {
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));

            if (path.Equals(Constants.Workspace)) return this;
            
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
            return (Project)projects[$"{Constants.Workspace}/{name}"];
        }

        /// <summary>
        /// Determines if the specified ID refers to a workspace document.
        /// </summary>
        /// <param name="id">A possible document ID.</param>
        /// <returns>true if the ID refers to a document in the workspace hierarchy; false otherwise.</returns>
        public bool IsDocument(string id)
        {
            return documents.ContainsKey(id);
        }

        /// <summary>
        /// Determines if the specified key refers to a workspace folder.
        /// </summary>
        /// <param name="key">A possible folder key.</param>
        /// <returns>true if the key refers to a folder in the workspace hierarchy; false otherwise.</returns>
        public bool IsFolder(string key)
        {
            return folders.ContainsKey(key);
        }

        /// <summary>
        /// Determines if the specified key refers to a workspace project.
        /// </summary>
        /// <param name="key">A possible project key.</param>
        /// <returns>true if the key refers to a project in the workspace hierarchy; false otherwise.</returns>
        public bool IsProject(string key)
        {
            return projects.ContainsKey(key);
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

            throw new ArgumentException(nameof(folder)); // TODO - Exception message
        }

        /// <summary>
        /// Determines if the workspace contains a document with the same name and path as the document provided.
        /// </summary>
        /// <param name="document">A <see cref="Document"/> that may have the same name and path as an existing document.</param>
        /// <returns>true if the workspace contains a document with the same name and path as the document provided; false otherwise.</returns>
        private bool DocumentExists(Document document)
        {
            foreach (var value in documents.Values)
            {
                if (value.Name == document.Name && value.Path == document.Path) return true;
            }

            return false;
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
