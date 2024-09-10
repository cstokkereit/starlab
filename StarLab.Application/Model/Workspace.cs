using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Workspace : IWorkspace
    {
        private readonly IDictionary<string, Folder> foldersByPath = new Dictionary<string, Folder>();

        private readonly List<IFolder> folders = new List<IFolder>();

        private string filename = string.Empty;

        private string layout = string.Empty;

        public Workspace(WorkspaceDTO dto)
        {
            filename = dto.FileName;

            if (dto.Folders != null)
            {
                CreateFolders(dto.Folders);
            }

            if (dto.Documents != null)
            {
                CreateDocuments(dto.Documents);
            }

            Dirty = false;
        }

        public Workspace() { }

        public bool Dirty { get; private set; }

        public string FileName
        {
            get { return filename; }

            set
            {
                filename = value;
                Dirty = true;
            }
        }

        public IEnumerable<IFolder> Folders => folders;

        public string Layout
        {
            get { return layout; }

            set
            {
                layout = value;
                Dirty = true;
            }
        }

        public IList<IDocument> Documents => throw new NotImplementedException();

        IList<IFolder> IWorkspace.Folders => throw new NotImplementedException();

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
                    Document document = new Document(dto);
                    foldersByPath[document.Path].AddDocument(document);
                    //documents.Add(document.ID, document);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos)
        {
            dtos = dtos.OrderBy(o => o.Path).ToList();

            foreach (var dto in dtos)
            {
                var path = dto.Path;

                if (!foldersByPath.ContainsKey(path))
                {
                    Folder folder;

                    if (path.Contains('/'))
                    {
                        folder = new Folder(dto, foldersByPath[path.Substring(0, path.LastIndexOf('/'))]);
                    }
                    else
                    {
                        folder = new Folder(dto);
                        folders.Add(folder);
                    }

                    foldersByPath.Add(folder.Path, folder);
                }
            }

            folders.AddRange(foldersByPath.Values);
        }

        #endregion
    }
}
