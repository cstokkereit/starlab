using StarLab.Presentation.Model;

namespace StarLab.Presentation.Docking
{
    public interface IDockableViewFactory
    {
        IDockableView CreateView(IDocument document);

        IDockableView GetView(string name);
    }
}
