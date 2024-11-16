using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal interface IFolder
    {
        IEnumerable<Document> Documents { get; }

        IEnumerable<IFolder> Folders { get; }

        string Name { get; set; }

        IFolder Parent { get; }

        string Path { get; }

        void AddDocument(Document document);

        void AddFolder(IFolder folder);

        void DeleteFolder(IFolder folder);
    }
}
