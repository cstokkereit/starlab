using StarLab.Application.DataTransfer;

namespace StarLab.Presentation.Model
{
    public class Workspace : IWorkspace
    {
        private readonly Dictionary<string, IDocument> documentsByName = new Dictionary<string, IDocument>();

        private readonly Dictionary<string, Folder> foldersByPath = new Dictionary<string, Folder>();

        private readonly List<IDocument> documents = new List<IDocument>();

        private readonly List<IFolder> folders = new List<IFolder>();

        #region Constructors

        public Workspace(WorkspaceDTO dto)
        {
            Layout = dto.Layout == null ? string.Empty : dto.Layout;

            Name = Path.GetFileNameWithoutExtension(dto.FileName);

            if (dto.Folders != null)
            {
                CreateFolders(dto.Folders);
            }
            
            if (dto.Documents != null)
            {
                CreateDocuments(dto.Documents);
            }

            AddChildFolders(foldersByPath.Values);

            // TODO
            ActiveDocument = documents[0];

        }

        public Workspace()
        {
            // Do Nothing
        }

        #endregion

        #region IWorkspace Members

        public IDocument? ActiveDocument { get; private set; }

        public IEnumerable<IDocument> Documents => documents;

        public IEnumerable<IFolder> Folders => folders;

        public string Layout { get; private set; }

        public string Name { get; private set; }

        public void ClearActiveDocument()
        {
            ActiveDocument = null;
        }

        public IDocument GetDocument(string name)
        {
            return documentsByName[name];
        }

        public bool HasDocument(string name)
        {
            return documentsByName.ContainsKey(name);
        }

        public void SetActiveDocument(string name)
        {
            if (documentsByName.ContainsKey(name)) ActiveDocument = documentsByName[name];
        }

        public void UpdateLayout(string layout)
        {
            Layout = layout;
        }

        #endregion

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
                documentsByName.Add(document.FullName, document);
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
