using System.ComponentModel;
using StarLab.Presentation;

namespace StarLab.Application
{
    public interface IFormViewPresenter : IPresenter, IViewController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void ViewClosing(CancelEventArgs e);
    }
}
