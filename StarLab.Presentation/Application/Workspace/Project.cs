using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class Project : IProject
    {
        private readonly List<IDocument> documents = new List<IDocument>();

        private readonly List<IFolder> folders = new List<IFolder>();

        private readonly Folder folder;

        public Project(ProjectDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentException();

            folder = new Folder(dto.Name, dto.Expanded);

            CreateFolders(dto.Folders);

            folder.AddChildFolders(folders);
        }

        public IEnumerable<IDocument> Documents => documents;

        public bool Expanded { get; private set; }

        public IEnumerable<IFolder> Folders => folders;

        public string Key => folder.Key;

        public string Name => folder.Name;

        public string ParentKey => folder.ParentKey;

        public void Collapse()
        {
            Expanded = false;
        }

        public void CollapseAll()
        {
            foreach (var folder in folder.Folders)
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
            foreach (var folder in folder.Folders)
            {
                folder.ExpandAll();
            }

            Expand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                folders.Add(new Folder(dto));
            }

            foreach (var folder in folders)
            {
                if (folder is Folder parent) parent.AddChildFolders(folders);
            }
        }
    }
}
