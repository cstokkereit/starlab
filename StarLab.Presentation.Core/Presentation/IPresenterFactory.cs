using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Model;

namespace StarLab.Presentation
{
    public interface IPresenterFactory
    {
        IControlViewPresenter CreatePresenter(IControlView view, string presenterTypeName);

        IControlViewPresenter CreatePresenter(IControlView view);

        IDockableViewPresenter CreatePresenter(IDockableView view, IDocument document);

        IDockableViewPresenter CreatePresenter(IDockableView view);

        IFormViewPresenter CreatePresenter(IView view);

        ISplitViewPresenter CreatePresenter(ISplitView view);
    }
}
