namespace StarLab.Application
{
    public interface IFormView : IView, IDialogController
    {
        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();
    }
}
