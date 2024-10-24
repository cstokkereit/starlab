using System.ComponentModel;

namespace StarLab.Application
{
    public interface IFormViewPresenter : IPresenter
    {
        void ViewClosing(CancelEventArgs args);
    }
}
