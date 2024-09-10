namespace StarLab.Presentation
{
    public interface IViewController : IController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        void Show(IView view);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="responses"></param>
        /// <returns></returns>
        //ShowMessage(string message, MessageType type, Responses responses);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        //void ShowMessage(string message, MessageType type);
    }
}
