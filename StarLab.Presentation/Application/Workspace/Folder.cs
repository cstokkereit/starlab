namespace StarLab.Application.Workspace
{
    public class Folder : IFolder
    {
        private List<Folder> folders = new List<Folder>();

        private readonly string parentKey;

        private readonly string name;

        private readonly string key;

        public Folder(FolderDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Path))
            {
                throw new ArgumentException(); // TODO
            }

            key = dto.Path;

            if (key.Contains('/'))
            {
                var index = key.LastIndexOf('/');
                parentKey = key.Substring(0, index);
                name = key.Substring(index + 1);
            }
            else
            {
                parentKey = string.Empty;
                name = key;
            }

            Expanded = dto.Expanded;
            IsNew = dto.IsNew;
        }

        public bool Expanded { get; private set; }

        public bool IsNew { get; private set; }

        public string Key => key;

        public string Name => name;

        public string ParentKey => parentKey;

        public string Path => parentKey + '/' + name;

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

        public void AddChildFolders(IEnumerable<Folder> folders)
        {
            foreach (var folder in folders)
            {
                if (folder.parentKey == key)
                {
                    this.folders.Add(folder);
                }
            }
        }
    }
}
