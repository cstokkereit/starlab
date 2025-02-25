using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents a workspace.
    /// </summary>
    internal class Workspace : IWorkspace
    {
        private readonly Dictionary<string, IDocument> documents = new Dictionary<string, IDocument>(); // A dictionary containing the documents indexed by ID.

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

            Layout = dto.Layout == null ? string.Empty : dto.Layout;

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

            if (!string.IsNullOrEmpty(dto.ActiveDocument) && documents.ContainsKey(dto.ActiveDocument)) ActiveDocument = documents[dto.ActiveDocument];
        }

        /// <summary>
        /// Gets the active <see cref="IDocument"/>.
        /// </summary>
        public IDocument? ActiveDocument { get; private set; }

        /// <summary>
        /// Gets the documents within the workspace.
        /// </summary>
        public IEnumerable<IDocument> Documents => documents.Values;

        /// <summary>
        /// Gets the workspace file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the folders within the workspace.
        /// </summary>
        public IEnumerable<IFolder> Folders => folders.Values;

        /// <summary>
        /// Gets the workspace layout.
        /// </summary>
        public string Layout { get; private set; }

        /// <summary>
        /// Gets the workspace name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the projects within the workspace.
        /// </summary>
        public IEnumerable<IProject> Projects => projects.Values;

        /// <summary>
        /// Clears the active document.
        /// </summary>
        public void ClearActiveDocument()
        {
            ActiveDocument = null;
        }

        /// <summary>
        /// Collapses all of the projects and folders within the workspace.
        /// </summary>
        public void Collapse()
        {
            foreach (var project in projects.Values)
            {
                project.Collapse();
            }
        }

        /// <summary>
        /// Expands all of the projects and folders within the workspace.
        /// </summary>
        public void Expand()
        {
            foreach (var project in projects.Values)
            {
                project.Expand();
            }
        }

        /// <summary>
        /// Gets the <see cref="IDocument"/> with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the required <see cref="IDocument"/>.</param>
        /// <returns>The <see cref="IDocument"/> with the specified ID.</returns>
        public IDocument GetDocument(string id)
        {
            return documents[id];
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
        public bool HasDocument(string id)
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
        public void SetActiveDocument(string id)
        {
            if (documents.ContainsKey(id)) ActiveDocument = documents[id];
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
