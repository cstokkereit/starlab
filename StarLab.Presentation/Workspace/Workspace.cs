using StarLab.Application.Workspace;
using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// View model representation of a workspace.
    /// </summary>
    internal class Workspace : IWorkspace
    {
        private readonly Dictionary<DocumentID, IDocument> documents = new Dictionary<DocumentID, IDocument>(); // A dictionary containing the documents indexed by ID.

        private readonly Dictionary<string, IProject> projects = new Dictionary<string, IProject>(); // A dictionary containing the projects indexed by key.

        private readonly Dictionary<string, IFolder> folders = new Dictionary<string, IFolder>(); // A dictionary containing the folders indexed by key.

        /// <summary>
        /// Initialises a new instance of the <see cref="Workspace"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> representation of the workspace.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Workspace(WorkspaceDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Layout = dto.Layout ?? string.Empty;

            if (!string.IsNullOrEmpty(dto.FileName))
            {
                Name = Path.GetFileNameWithoutExtension(dto.FileName);
                FileName = dto.FileName;
            }
            else
            {
                FileName = string.Empty;
                Name = string.Empty;
            }

            CreateProjects(dto.Projects);

            if (!string.IsNullOrEmpty(dto.ActiveDocument))
            {
                if (documents.TryGetValue(new DocumentID(dto.ActiveDocument), out IDocument? document))
                {
                    ActiveDocument = document;
                }
            }

            if (!string.IsNullOrEmpty(dto.SelectedFolder))
            {
                if (folders.TryGetValue(dto.SelectedFolder, out IFolder? folder))
                {
                    SelectedFolder = folder;
                }
            }
        }

        /// <summary>
        /// Gets the active <see cref="IDocument"/>.
        /// </summary>
        public IDocument? ActiveDocument { get; private set; }

        /// <summary>
        /// Gets the documents within the workspace.
        /// </summary>
        public IEnumerable<IDocument> Documents => documents.Values.OrderBy(document => document.Name);

        /// <summary>
        /// Returns true if the workspace is expanded; false otherwise.
        /// </summary>
        public bool Expanded => true;

        /// <summary>
        /// Gets the workspace file name.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the folders within the workspace.
        /// </summary>
        public IEnumerable<IFolder> Folders => folders.Values.OrderBy(folder => folder.Key);

        /// <summary>
        /// Gets the workspace key.
        /// </summary>
        public string Key => Constants.Workspace;

        /// <summary>
        /// Gets the workspace layout.
        /// </summary>
        public string Layout { get; private set; }

        /// <summary>
        /// Gets the workspace name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the parent folder key.
        /// </summary>
        public string ParentKey => string.Empty;

        /// <summary>
        /// Gets the projects within the workspace.
        /// </summary>
        public IEnumerable<IProject> Projects => projects.Values.OrderBy(project => project.Name);

        /// <summary>
        /// Gets the selected <see cref="IFolder"/>.
        /// </summary>
        public IFolder? SelectedFolder { get; private set; }

        /// <summary>
        /// Clears the active document.
        /// </summary>
        public void ClearActiveDocument()
        {
            ActiveDocument = null;
        }

        /// <summary>
        /// Collapses the workspace.
        /// </summary>
        public void Collapse()
        {
            // Do Nothing
        }

        /// <summary>
        /// Recursively collapses the workspace and all of its children.
        /// </summary>
        public void CollapseAll()
        {
            foreach (var project in projects.Values)
            {
                project.Collapse();
            }
        }

        /// <summary>
        /// Expands the workspace.
        /// </summary>
        public void Expand()
        {
            // Do Nothing
        }

        /// <summary>
        /// Recursively expands the workspace and all of its children.
        /// </summary>
        public void ExpandAll()
        {
            foreach (var project in projects)
            {
                project.Value.ExpandAll();
            }
        }

        /// <summary>
        /// Gets the <see cref="IDocument"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IDocument"/>.</param>
        /// <returns>The <see cref="IDocument"/> with the specified ID.</returns>
        public IDocument GetDocument(DocumentID id)
        {
            return documents[id];
        }

        /// <summary>
        /// Gets a <see cref="List{String}"/> containing the IDs of all documents in the workspace hierarchy.
        /// </summary>
        /// <returns>A <see cref="List{String}"/> of document IDs.</returns>
        public List<DocumentID> GetDocumentIDs()
        {
            var ids = new List<DocumentID>();

            foreach (var id in documents.Keys)
            {
                ids.Add(id);
            }

            return ids;
        }

        /// <summary>
        /// Gets the <see cref="IFolder"/> with the specified key.
        /// </summary>
        /// <param name="key">The key of the required <see cref="IFolder"/>.</param>
        /// <returns>The <see cref="IFolder"/> with the specified key.</returns>
        public IFolder GetFolder(string key)
        {
            return folders[key];
        }

        /// <summary>
        /// Gets the <see cref="IProject"/> with the specified key.
        /// </summary>
        /// <param name="key">The key of the required <see cref="IProject"/>.</param>
        /// <returns>The <see cref="IProject"/> with the specified key.</returns>
        public IProject GetProject(string key)
        {
            return projects[key];
        }

        /// <summary>
        /// Determines if the workspace contains the specified document.
        /// </summary>
        /// <param name="id">The ID of the required document.</param>
        /// <returns>true if the workspace contains a document with the specified ID; false otherwise.</returns>
        public bool HasDocument(DocumentID id)
        {
            return documents.ContainsKey(id);
        }

        /// <summary>
        /// Determines if the workspace contains the specified folder.
        /// </summary>
        /// <param name="key">The key of the required folder.</param>
        /// <returns>true if the workspace contains a folder with the specified key; false otherwise.</returns>
        public bool HasFolder(string key)
        {
            return folders.ContainsKey(key);
        }

        /// <summary>
        /// Determines if the workspace contains the specified project.
        /// </summary>
        /// <param name="key">The key of the required project.</param>
        /// <returns>true if the workspace contains a project with the specified key; false otherwise.</returns>
        public bool HasProject(string key)
        {
            return projects.ContainsKey(key);
        }

        /// <summary>
        /// Sets the active document to be the <see cref="IDocument"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the active <see cref="IDocument"/>.</param>
        public void SetActiveDocument(DocumentID id)
        {
            if (documents.TryGetValue(id, out IDocument? document))
            {
                SetSelectedFolder(document.Path);
                ActiveDocument = document;
            }
        }

        /// <summary>
        /// Sets the selected folder to be the <see cref="IFolder"/> with the specified path.
        /// </summary>
        /// <param name="path">The path of the selected folder.</param>
        public void SetSelectedFolder(string path)
        {
            if (path == Constants.Workspace)
            {
                SelectedFolder = null;
            }
            else if (folders.TryGetValue(path, out IFolder? folder))
            {
                SelectedFolder = folder;
            }
            else if (projects.TryGetValue(path, out IProject? project))
            {
                SelectedFolder = project;
            }
            else if (documents.TryGetValue(new DocumentID(path), out IDocument? document))
            {
                SetSelectedFolder(document.Path);
            }
        }

        /// <summary>
        /// Updates the workspace layout.
        /// </summary>
        /// <param name="layout">The new layout.</param>
        public void UpdateLayout(string layout)
        {
            Layout = layout;
        }

        /// <summary>
        /// Creates the projects that belong to this workspace from the <see cref="ProjectDTO"/>s provided.
        /// </summary>
        /// <param name="dtos">An <see cref="IEnumerable{ProjectDTO}"/> that contains the dtos.</param>
        private void CreateProjects(IEnumerable<ProjectDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var project = new Project(dto);

                projects.Add(project.Key, project);

                foreach (var folder in project.Folders)
                {
                    folders.Add(folder.Key, folder);
                }

                foreach (var document in project.Documents)
                {
                    documents.Add(document.ID, document);
                }
            }
        }
    }
}
