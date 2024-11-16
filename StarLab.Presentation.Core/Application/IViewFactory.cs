using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public interface IViewFactory
    {
        IControlView CreateControlView(string typeName);

        IDockableView CreateDocumentView(IDocument document);

        IDialogView CreateDialogView(string id, string text);

        IDockableView CreateToolView(string id, string text);

        IWorkspaceView CreateWorkspaceView();
    }
}
