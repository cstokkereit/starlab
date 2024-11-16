using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public interface IPresenterFactory
    {
        IChildViewPresenter CreatePresenter(IChildView view);

        IDockableViewPresenter CreatePresenter(IDockableView view);

        IDockableViewPresenter CreatePresenter(IDocumentView view, IDocument document);

        IDialogViewPresenter CreatePresenter(IDialogView view);

        IWorkspaceViewPresenter CreatePresenter(IWorkspaceView view);
    }
}
