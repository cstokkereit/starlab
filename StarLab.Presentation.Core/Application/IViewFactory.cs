using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public interface IViewFactory
    {
        IChildView CreateControlView(string typeName);

        IViewBundle CreateDocumentView(IDocument document);

        IViewBundle CreateDialogView(string id, string text);

        IViewBundle CreateToolView(string id, string text);

        IViewBundle CreateWorkspaceView();
    }
}
