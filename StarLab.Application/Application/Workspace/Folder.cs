using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class Folder : IFolder
    {
        private readonly List<Document> documents = new List<Document>();

        private readonly List<IFolder> folders = new List<IFolder>();

        private readonly bool expanded;

        private readonly bool isNew;

        public Folder(FolderDTO dto, IFolder parent)
        {
            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentException(); // TODO

            Parent = parent ?? throw new ArgumentNullException(nameof(parent));

            Name = dto.Path.Substring(dto.Path.LastIndexOf('/') + 1);
            
            expanded = dto.Expanded;

            Parent.AddFolder(this);
        }

        public Folder(string? name, bool expanded, IFolder parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            this.expanded = expanded;
        }

        public Folder(string name, IFolder parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Name = name ?? throw new ArgumentNullException(nameof(name));

            isNew = true;

            Parent.AddFolder(this);
        }

        public IEnumerable<Document> Documents => documents;

        public bool Expanded => expanded;

        public IEnumerable<IFolder> Folders => folders;

        public bool IsNew => isNew;

        public string Name { get; set; }

        public string Path
        {
            get { return $"{Parent.Path}/{Name}"; }
        }

        public IFolder Parent { get; private set; }

        public void AddDocument(Document document)
        {
            documents.Add(document);
        }

        public void AddFolder(IFolder folder)
        {
            folders.Add(folder);
        }

        public void DeleteDocument(Document document)
        {
            documents.Remove(document);
        }

        public void DeleteFolder(IFolder folder)
        {
            folders.Remove(folder);
        }
    }
}
