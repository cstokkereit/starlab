using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    public interface IDockableViewFactory
    {
        IDockableView CreateView(IDocument document);

        IDockableView GetView(string id);
    }
}
