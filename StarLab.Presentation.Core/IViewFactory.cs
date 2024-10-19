using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Model;

namespace StarLab
{
    public interface IViewFactory
    {
        IControlView CreateControlView(string typeName);

        IDockableView CreateDocumentView(IDocument document);

        IFormView CreateFormView(string id, string name);

        IDockableView CreateToolView(string id, string name);
    }
}
