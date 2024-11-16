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
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);
    }
}
