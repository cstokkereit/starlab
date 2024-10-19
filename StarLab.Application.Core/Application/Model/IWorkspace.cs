namespace StarLab.Application.Model
{
    public interface IWorkspace
    {
        IEnumerable<IDocument> Documents { get; }

        IEnumerable<IFolder> Folders { get; }

        void DeleteDocument(string id);

        void DeleteFolder(string path);

        IDocument GetDocument(string id);

        IFolder GetFolder(string path);

        void RenameFolder(IFolder folder, string name);
    }
}
