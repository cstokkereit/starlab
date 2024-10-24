namespace StarLab.Application
{
    public interface IOutputPort
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void ShowErrorMessage(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void ShowWarningMessage(string message);
    }
}
