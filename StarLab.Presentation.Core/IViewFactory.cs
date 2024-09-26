using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Model;

namespace StarLab
{
    public interface IViewFactory
    {
        IDockableView CreateDocumentView(IDocument document);

        IDockableView CreateToolView(string name);

        IFormView CreateView(string name);
    }
}
