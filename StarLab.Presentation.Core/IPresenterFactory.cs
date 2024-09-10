using StarLab.Presentation.Docking;
using StarLab.Presentation.Model;

namespace StarLab.Presentation
{
    public interface IPresenterFactory
    {
        IControlViewPresenter CreatePresenter(IControlView view, string presenterTypeName);

        IControlViewPresenter CreatePresenter(IControlView view);

        IDockableViewPresenter CreatePresenter(IControlView content, IDockableView view);

        IDockableViewPresenter CreatePresenter(IDockableView view, IDocument document);

        IFormViewPresenter CreatePresenter(IView view);

        ISplitViewPresenter CreatePresenter(ISplitView view);
    }
}
