using StarLab.Presentation.Docking;

namespace StarLab.Presentation
{
    public interface IViewFactory
    {
        IDockableView CreateView(IViewContext context);

        IFormView CreateView(string name);
    }
}
