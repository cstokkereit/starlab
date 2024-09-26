namespace StarLab.Application.Model
{
    public interface IWorkspace
    {
        //IEnumerable<IFolder> Folders { get; }

        IDocument GetDocument(string id);

        IFolder GetFolder(string path);
    }
}
