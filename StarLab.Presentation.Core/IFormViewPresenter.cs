using System.ComponentModel;

namespace StarLab.Presentation
{
    public interface IFormViewPresenter : IPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void ViewClosing(CancelEventArgs e);
    }
}
