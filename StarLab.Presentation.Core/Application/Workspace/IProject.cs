using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    public interface IProject : ICollapsible
    {
        IEnumerable<IDocument> Documents { get; }

        IEnumerable<IFolder> Folders { get; }

        string Key { get; }

        string Name { get; }

        string ParentKey { get; }
    }
}
