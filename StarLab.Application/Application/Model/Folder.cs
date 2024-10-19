using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    internal class Folder : IFolder
    {
        private const string PATH = "{0}/{1}";

        private const string WORKSPACE = "Workspace";
        
        private readonly List<IDocument> documents = new List<IDocument>();

        private readonly List<IFolder> folders = new List<IFolder>();

        private readonly bool expanded;

        private readonly bool isNew = false;

        public Folder(FolderDTO dto, IFolder parent)
        {
            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentException(); // TODO

            ArgumentNullException.ThrowIfNull(nameof(parent));

            Name = dto.Path.Substring(dto.Path.LastIndexOf('/') + 1);

            expanded = dto.Expanded;

            Parent = parent;

            parent.AddFolder(this);
        }

        public Folder(FolderDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentException(); // TODO

            Name = dto.Path.Substring(dto.Path.LastIndexOf('/') + 1);
            
            expanded = dto.Expanded;
        }

        public Folder(string name, IFolder parent)
        {
            Parent = parent;
            Name = name;

            isNew = true;

            parent.AddFolder(this);
        }

        public IEnumerable<IDocument> Documents => documents;

        public bool Expanded => expanded;

        public IEnumerable<IFolder> Folders => folders;

        public bool IsNew => isNew;

        public string Name { get; set; }

        public IFolder? Parent { get; private set; }

        public string Path
        {
            get { return string.Format(PATH, Parent == null ? WORKSPACE : Parent.Path, Name); }
        }

        public void AddDocument(IDocument document)
        {
            documents.Add(document);
        }

        public void AddFolder(IFolder folder)
        {
            folders.Add(folder);
        }

        public void DeleteContents()
        {
            documents.Clear();
            folders.Clear();
            Parent = null;
        }
    }
}
