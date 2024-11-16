namespace StarLab.Application.Workspace
{
    internal class Folder : IFolder
    {
        private readonly List<IFolder> folders = new List<IFolder>();

        private readonly string parentKey;

        private readonly string name;

        private readonly string key;

        public Folder(FolderDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            if (string.IsNullOrEmpty(dto.Path)) throw new ArgumentException(); // TODO

            key = dto.Path;

            var index = key.LastIndexOf('/');
            parentKey = key.Substring(0, index);
            name = key.Substring(index + 1);

            Expanded = dto.Expanded;
            IsNew = dto.IsNew;
        }

        public Folder(string name, bool expanded)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(); // TODO

            this.name = name;

            parentKey = Constants.WORKSPACE;
            key = parentKey + '/' + name;
            
            Expanded = expanded;
        }

        public bool Expanded { get; private set; }

        public IEnumerable<IFolder> Folders => folders;

        public bool IsNew { get; private set; }

        public string Key => key;

        public string Name => name;

        public string ParentKey => parentKey;

        public void Collapse()
        {
            Expanded = false;
        }

        public void CollapseAll()
        {
            foreach (var folder in folders)
            {
                folder.CollapseAll();
            }

            Collapse();
        }

        public void Expand()
        {
            Expanded = true;
        }

        public void ExpandAll()
        {
            foreach (var folder in folders)
            {
                folder.ExpandAll();
            }

            Expand();
        }

        public void AddChildFolders(IEnumerable<IFolder> folders)
        {
            foreach (var folder in folders)
            {
                if (folder.ParentKey == key) this.folders.Add(folder);
            }
        }
    }
}
