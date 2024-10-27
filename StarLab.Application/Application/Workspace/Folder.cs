using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class Folder
    {
        private const string PATH = "{0}/{1}";

        private const string WORKSPACE = "Workspace";

        private readonly List<Document> documents = new List<Document>();

        private readonly List<Folder> folders = new List<Folder>();

        private readonly bool expanded;

        private readonly bool isNew = false;

        public Folder(FolderDTO dto, Folder parent)
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

        public Folder(string name, Folder parent)
        {
            Parent = parent;
            Name = name;

            isNew = true;

            parent.AddFolder(this);
        }

        public IEnumerable<Document> Documents => documents;

        public bool Expanded => expanded;

        public IEnumerable<Folder> Folders => folders;

        public bool IsNew => isNew;

        public string Name { get; set; }

        public Folder? Parent { get; private set; }

        public string Path
        {
            get { return string.Format(PATH, Parent == null ? WORKSPACE : Parent.Path, Name); }
        }

        public void AddDocument(Document document)
        {
            documents.Add(document);
        }

        public void AddFolder(Folder folder)
        {
            folders.Add(folder);
        }

        public void DeleteDocument(Document document)
        {
            documents.Remove(document);
        }

        public void DeleteFolder(Folder folder)
        {
            folders.Remove(folder);
        }
    }
}
