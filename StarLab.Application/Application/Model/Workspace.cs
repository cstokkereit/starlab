using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Workspace : IWorkspace
    {
        private readonly Dictionary<string, IDocument> documentsByID = new Dictionary<string, IDocument>();

        private readonly Dictionary<string, Folder> foldersByPath = new Dictionary<string, Folder>();

        //private readonly List<IFolder> folders = new List<IFolder>();

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

        //public IEnumerable<IFolder> Folders => folders;

        public IDocument GetDocument(string id)
        {
            return documentsByID[id];
        }

        public IFolder GetFolder(string path)
        {
            return foldersByPath[path];
        }

        #region Private Members

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
                    //documents.Add(document);
                }
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
                foldersByPath.Add(folder.Path, folder);
                //folders.Add(folder);
            }
        }

        #endregion
    }
}
