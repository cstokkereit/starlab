namespace StarLab.Application
{
    public interface IViewController : IController, IDialogController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        void Show(IView view);
    }
}
