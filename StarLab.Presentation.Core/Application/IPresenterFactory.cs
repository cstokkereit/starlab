using StarLab.Application.Configuration;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public interface IPresenterFactory
    {
        IPresenter CreatePresenter(string name, IView view);

        IPresenter CreatePresenter(IDocument document, IView view);

        IPresenter CreatePresenter(IViewConfiguration parent, IChildView child);
    }
}
