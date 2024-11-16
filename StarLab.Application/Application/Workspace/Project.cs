using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class Project : IFolder
    {
        private IFolder folder;

        public Project(ProjectDTO dto, IFolder parent)
        {
            folder = new Folder(dto.Name, dto.Expanded, parent);
        }

        public IEnumerable<Document> Documents => folder.Documents;

        public IEnumerable<IFolder> Folders => folder.Folders;

        public string Name { get => folder.Name; set => folder.Name = value; }

        public IFolder Parent => folder.Parent;

        public string Path => folder.Path;

        public void AddDocument(Document document)
        {
            folder.AddDocument(document);
        }

        public void AddFolder(IFolder folder)
        {
            if (folder is not Folder) throw new InvalidOperationException(); // TODO

            this.folder.AddFolder(folder);
        }

        public void DeleteFolder(IFolder folder)
        {
            if (folder is not Folder) throw new InvalidOperationException(); // TODO

            this.folder.DeleteFolder(folder);
        }
    }
}
