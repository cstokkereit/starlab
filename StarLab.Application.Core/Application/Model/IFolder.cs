namespace StarLab.Application.Model
{
    public interface IFolder
    {
        IEnumerable<IDocument> Documents { get; }

        IEnumerable<IFolder> Folders { get; }

        string Name { get; set; }

        string Path { get; }

        void AddDocument(IDocument document);

        void AddFolder(IFolder folder);
    }
}
