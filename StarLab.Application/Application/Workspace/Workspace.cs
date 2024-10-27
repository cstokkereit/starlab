using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class Workspace
    {
        private readonly Dictionary<string, Document> documentsByID = new Dictionary<string, Document>();

        private readonly Dictionary<string, Folder> foldersByPath = new Dictionary<string, Folder>();

        public Workspace(WorkspaceDTO dto)
        {
            if (dto.Folders != null)
            {
                CreateFolders(dto.Folders);
            }

            if (dto.Documents != null)
            {
                CreateDocuments(dto.Documents);
            }
        }

        public Workspace() { }
        public IEnumerable<Document> Documents => documentsByID.Values;

        public IEnumerable<Folder> Folders => foldersByPath.Values;

        public void AddFolder(Folder folder)
        {
            if (!foldersByPath.ContainsKey(folder.Path)) foldersByPath.Add(folder.Path, folder);
        }

        public void DeleteDocument(string id)
        {
            var document = GetDocument(id);

            if (document != null && foldersByPath.ContainsKey(document.Path))
            {
                var folder = foldersByPath[document.Path];

                if (folder != null)
                {
                    folder.DeleteDocument(document);
                    documentsByID.Remove(id);
                }
            }
        }

        public void DeleteFolder(string path)
        {
            DeleteFolders(GetFolder(path));
        }

        public Document GetDocument(string id)
        {
            return documentsByID[id];
        }

        public Folder GetFolder(string path)
        {
            return foldersByPath[path];
        }

        public void RenameFolder(Folder folder, string name)
        {
            var folders = new List<Folder>(foldersByPath.Values);
            if (folder is Folder f) f.Name = name;
            UpdatePaths(folders);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateDocuments(IEnumerable<DocumentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Path) && foldersByPath.ContainsKey(dto.Path))
                {
                    var document = new Document(dto);
                    foldersByPath[document.Path].AddDocument(document);
                    documentsByID.Add(document.ID, document);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos)
        {
            Folder folder;

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Path))
                {
                    var parentPath = dto.Path.Substring(0, dto.Path.LastIndexOf('/'));

                    if (foldersByPath.ContainsKey(parentPath))
                        folder = new Folder(dto, foldersByPath[parentPath]);
                    else
                        folder = new Folder(dto);

                    foldersByPath.Add(folder.Path, folder);
                }
            }
        }

        private void DeleteFolders(Folder folder)
        {
            foreach (var document in folder.Documents)
            {
                documentsByID.Remove(document.ID);
            }

            foreach (var child in folder.Folders)
            {
                foldersByPath.Remove(child.Path);

                DeleteFolders(child);
            }

            if (folder.Parent != null && foldersByPath.ContainsKey(folder.Parent.Path)) foldersByPath[folder.Parent.Path].DeleteFolder(folder);
            
            foldersByPath.Remove(folder.Path);
        }

        private void UpdatePaths(IEnumerable<Folder> folders)
        {
            foldersByPath.Clear();

            foreach (var folder in folders)
            {
                foreach (var document in folder.Documents)
                {
                    document.Path = folder.Path;
                }

                foldersByPath.Add(folder.Path, folder);
            }
        }
    }
}
