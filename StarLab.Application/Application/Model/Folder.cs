using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    internal class Folder : IFolder
    {
        private const string PATH = "{0}/{1}";

        private readonly IList<IDocument> documents = new List<IDocument>();

        private readonly IList<IFolder> folders = new List<IFolder>();

        private string name = string.Empty;

        #region Constructors

        public Folder(FolderDTO dto, IFolder parent)
        {
            Name = dto.Path == null ? string.Empty : dto.Path.Substring(dto.Path.LastIndexOf('/') + 1);

            if (parent != null)
            {
                parent.AddFolder(this);
                Parent = parent;
            }
        }

        public Folder(FolderDTO dto)
        {
            Name = dto.Path == null ? string.Empty : dto.Path;
        }

        #endregion

        #region IFolder Members

        public IEnumerable<IDocument> Documents => documents;

        public IEnumerable<IFolder> Folders => folders;

        public string Name { get; set; }

        public IFolder? Parent { get; private set; }

        public string Path
        {
            get { return Parent == null ? Name : string.Format(PATH, Parent.Path, Name); }
        }

        public void AddDocument(IDocument document)
        {
            documents.Add(document);
        }

        public void AddFolder(IFolder folder)
        {
            folders.Add(folder);
        }

        #endregion
    }
}
