namespace StarLab.Application
{
    public interface IViewController : IController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        void Show(IView view);
    }
}
