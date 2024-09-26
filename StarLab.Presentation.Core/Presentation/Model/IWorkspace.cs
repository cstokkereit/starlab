namespace StarLab.Presentation.Model
{
    public interface IWorkspace
    {
        IDocument? ActiveDocument { get; }

        IEnumerable<IDocument> Documents { get; }

        IEnumerable<IFolder> Folders { get; }

        string Layout { get; }

        string Name { get; }

        void ClearActiveDocument();

        void Collapse();

        void Expand();

        IDocument GetDocument(string id);

        IFolder GetFolder(string path);

        bool HasDocument(string id);

        void SetActiveDocument(string id);

        void UpdateLayout(string layout);
    }
}
