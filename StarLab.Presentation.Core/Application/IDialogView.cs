namespace StarLab.Application
{
    public interface IDialogView : IView
    {
        /// <summary>
        /// 
        /// </summary>
        bool HideOnClose { get; set; }

        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        void Initialise(IApplicationController controller);
    }
}
