using StarLab.Application.Configuration;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public interface IViewFactory : IPresenterFactory
    {
        IView CreateView(string name, string text);

        IView CreateView(IDocument document);

        IChildView CreateView(IContentConfiguration config, IViewConfiguration parent);
    }
}
