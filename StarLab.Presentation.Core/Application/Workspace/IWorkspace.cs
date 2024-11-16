using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    public interface IWorkspace
    {
        IDocument? ActiveDocument { get; }

        string FileName { get; }

        string Layout { get; }

        string Name { get; }

        IEnumerable<IDocument> Documents { get; }

        IEnumerable<IFolder> Folders { get; }

        IEnumerable<IProject> Projects { get; }

        void ClearActiveDocument();

        void Collapse();

        void Expand();

        IDocument GetDocument(string id);

        IFolder GetFolder(string key);

        IProject GetProject(string key);

        bool HasDocument(string id);

        bool HasFolder(string key);

        bool HasProject(string key);

        void SetActiveDocument(string id);

        void UpdateLayout(string layout);
    }
}
