using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    public class Workspace : IWorkspace
    {
        private readonly Dictionary<string, IDocument> documentsByID = new Dictionary<string, IDocument>();

        private readonly Dictionary<string, Folder> foldersByPath = new Dictionary<string, Folder>();

        private readonly List<IDocument> documents = new List<IDocument>();

        private readonly List<IFolder> folders = new List<IFolder>();

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

            if (dto.Folders != null) CreateFolders(dto.Folders);

            if (dto.Documents != null) CreateDocuments(dto.Documents);

            AddChildFolders(foldersByPath.Values);

            // TODO
            ActiveDocument = documents[0];
        }

        public Workspace()
        {
            // Do Nothing
        }

        public IDocument? ActiveDocument { get; private set; }

        public IEnumerable<IDocument> Documents => documents;

        public string FileName { get; private set; }

        public IEnumerable<IFolder> Folders => folders;

        public string Layout { get; private set; }

        public string Name { get; private set; }

        public void ClearActiveDocument()
        {
            ActiveDocument = null;
        }

        public void Collapse()
        {
            foreach (var folder in folders)
            {
                folder.Collapse();
            }
        }

        public void Expand()
        {
            foreach (var folder in folders)
            {
                folder.Expand();
            }
        }

        public IDocument GetDocument(string id)
        {
            return documentsByID[id];
        }

        public IFolder GetFolder(string path)
        {
            return foldersByPath[path];
        }

        public bool HasDocument(string id)
        {
            return documentsByID.ContainsKey(id);
        }

        public void SetActiveDocument(string id)
        {
            if (documentsByID.ContainsKey(id)) ActiveDocument = documentsByID[id];
        }

        public void UpdateLayout(string layout)
        {
            Layout = layout;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folders"></param>
        private void AddChildFolders(IEnumerable<Folder> folders)
        {
            foreach (var folder in folders)
            {
                folder.AddChildFolders(folders);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateDocuments(IEnumerable<DocumentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var document = new Document(dto);
                documentsByID.Add(document.ID, document);
                documents.Add(document);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var folder = new Folder(dto);
                foldersByPath.Add(folder.Key, folder);
                folders.Add(folder);
            }
        }
    }
}
