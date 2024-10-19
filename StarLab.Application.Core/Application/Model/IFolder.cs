namespace StarLab.Application.Model
{
    public interface IFolder
    {
        IEnumerable<IDocument> Documents { get; }

        bool Expanded { get; }

        IEnumerable<IFolder> Folders { get; }

        string Name { get; }

        IFolder? Parent { get; }

        string Path { get; }

        void AddDocument(IDocument document);

        void AddFolder(IFolder folder);

        void DeleteContents();
    }
}
