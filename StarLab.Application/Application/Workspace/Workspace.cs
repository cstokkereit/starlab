using StarLab.Application.Workspace.Documents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace StarLab.Application.Workspace
{
    internal class Workspace : IFolder
    {
        private readonly Dictionary<string, Document> documents = new Dictionary<string, Document>();

        private readonly Dictionary<string, Project> projects = new Dictionary<string, Project>();

        private readonly Dictionary<string, IFolder> folders = new Dictionary<string, IFolder>();

        public Workspace(WorkspaceDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            CreateProjects(dto.Projects);
        }

        public Workspace() { }

        public IEnumerable<Document> Documents => throw new InvalidOperationException();

        public IEnumerable<IFolder> Folders => throw new InvalidOperationException();

        public string Name { get => Constants.WORKSPACE; set => throw new InvalidOperationException(); }

        public string Path { get => Constants.WORKSPACE; }

        public IFolder Parent => throw new InvalidOperationException();

        public IEnumerable<Project> Projects => projects.Values;

        public void AddDocument(Document document)
        {
            var folder = GetFolder(document.Path);
            folder.AddDocument(document);

            documents.Add(document.ID, document);
        }

        public void AddFolder(IFolder folder)
        {
            if (folder is not Folder) throw new InvalidOperationException();

            folders.Add(folder.Path, folder);
        }

        public void DeleteDocument(string id)
        {
            //var document = GetDocument(id);

            //if (document != null && folders.ContainsKey(document.Path))
            //{
            //    var folder = folders[document.Path];

            //    if (folder != null)
            //    {
            //        folder.DeleteDocument(document);
            //        documents.Remove(id);
            //    }
            //}
        }

        public void DeleteFolder(IFolder folder)
        {
            if (folder != null && folder.Parent != null)
            {
                DeleteFolders(folder);

                
                folder.Parent.DeleteFolder(folder);
            }
        }

        public void DeleteFolder(string path)
        {
            DeleteFolder(GetFolder(path));
        }


        public Document GetDocument(string id)
        {
            return documents[id];
        }

        public IFolder GetFolder(string path)
        {
            if (path.Equals(Constants.WORKSPACE)) return this;
            
            if (projects.ContainsKey(path)) return projects[path];
            
            if (folders.ContainsKey(path)) return folders[path];
            
            throw new KeyNotFoundException(path);
        }

        public Project GetProject(string name)
        {
            return projects[$"{Constants.WORKSPACE}/{name}"];
        }

        public void RenameFolder(IFolder folder, string name)
        {
            var folders = new List<IFolder>(this.folders.Values);

            folder.Name = name;

            UpdatePaths(folders);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateDocuments(IEnumerable<DocumentDTO> dtos)
        {
            //foreach (var dto in dtos)
            //{
            //    if (!string.IsNullOrEmpty(dto.Path) && folders.ContainsKey(dto.Path))
            //    {
            //        var document = new Document(dto);
            //        folders[document.Path].AddDocument(document);
            //        documents.Add(document.ID, document);
            //    }
            //}
        }

        private void CreateProjects(IEnumerable<ProjectDTO> dtos)
        {
            // Validate paths of new IFolder objects against their dto.Path?

            foreach (var dto in dtos)
            {
                var project = new Project(dto, this);
                projects.Add(project.Path, project);
                CreateFolders(dto.Folders, project);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos, IFolder parent)
        {
            Folder folder;

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Path))
                {
                    var parentPath = dto.Path.Substring(0, dto.Path.LastIndexOf('/'));

                    if (folders.ContainsKey(parentPath))
                        folder = new Folder(dto, folders[parentPath]);
                    else
                        folder = new Folder(dto, parent);

                    folders.Add(folder.Path, folder);
                }
            }
        }

        private void DeleteDocuments(IFolder folder)
        {
            foreach (var document in folder.Documents)
            {
                documents.Remove(document.ID);
            }
        }

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

        private void UpdatePaths(IEnumerable<IFolder> folders)
        {
            this.folders.Clear();

            foreach (var folder in folders)
            {
                foreach (var document in folder.Documents)
                {
                    document.Path = folder.Path;
                }

                this.folders.Add(folder.Path, folder);
            }
        }
    }
}
