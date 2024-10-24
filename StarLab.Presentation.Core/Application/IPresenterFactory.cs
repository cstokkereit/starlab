using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public interface IPresenterFactory
    {
        IControlViewPresenter CreatePresenter(IControlView view);

        IDockableViewPresenter CreatePresenter(IDockableView view, string id, string name);

        IDockableViewPresenter CreatePresenter(IDocumentView view, IDocument document);

        IFormViewPresenter CreatePresenter(IFormView view);

        IWorkspaceViewPresenter CreatePresenter(IWorkspaceView view);
    }
}
