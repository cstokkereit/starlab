using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class Workspace : IWorkspace
    {
        private readonly Dictionary<string, IDocument> documents = new Dictionary<string, IDocument>();

        private readonly Dictionary<string, IProject> projects = new Dictionary<string, IProject>();

        private readonly Dictionary<string, IFolder> folders = new Dictionary<string, IFolder>();

        public Workspace(WorkspaceDTO dto)
        {
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

            if (dto.Projects != null) CreateProjects(dto.Projects);

            if (!string.IsNullOrEmpty(dto.ActiveDocument) && documents.Count > 0) ActiveDocument = documents[dto.ActiveDocument];
        }

        public IDocument? ActiveDocument { get; private set; }

        public IEnumerable<IDocument> Documents => documents.Values;

        public string FileName { get; private set; }

        public IEnumerable<IFolder> Folders => folders.Values;

        public string Layout { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<IProject> Projects => projects.Values;

        public void ClearActiveDocument()
        {
            ActiveDocument = null;
        }

        public void Collapse()
        {
            foreach (var project in projects.Values)
            {
                project.Collapse();
            }
        }

        public void Expand()
        {
            foreach (var project in projects.Values)
            {
                project.Expand();
            }
        }

        public IDocument GetDocument(string id)
        {
            return documents[id];
        }

        public IFolder GetFolder(string key)
        {
            return folders[key];
        }

        public IProject GetProject(string key)
        {
            return projects[key];
        }

        public bool HasDocument(string id)
        {
            return documents.ContainsKey(id);
        }

        public bool HasFolder(string key)
        {
            return folders.ContainsKey(key);
        }

        public bool HasProject(string key)
        {
            return projects.ContainsKey(key);
        }

        public void SetActiveDocument(string id)
        {
            if (documents.ContainsKey(id)) ActiveDocument = documents[id];
        }

        public void UpdateLayout(string layout)
        {
            Layout = layout;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
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
            }
        }
    }
}
