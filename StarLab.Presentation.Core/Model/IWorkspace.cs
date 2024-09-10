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

        IDocument GetDocument(string name);

        bool HasDocument(string name);

        void SetActiveDocument(string name);

        void UpdateLayout(string layout);
    }
}
