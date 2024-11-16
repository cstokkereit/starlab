using System.ComponentModel;

namespace StarLab.Application
{
    public interface IDialogViewPresenter : IPresenter
    {
        void ViewClosing(CancelEventArgs args);
    }
}
