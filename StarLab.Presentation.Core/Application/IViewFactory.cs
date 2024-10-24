using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public interface IViewFactory
    {
        IControlView CreateControlView(string typeName);

        IDockableView CreateDocumentView(IDocument document);

        IFormView CreateFormView(string id, string name);

        IDockableView CreateToolView(string id, string name);
    }
}
