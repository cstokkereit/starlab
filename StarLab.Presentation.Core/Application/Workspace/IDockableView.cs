namespace StarLab.Application.Workspace
{
    public interface IDockableView : IView, IDialogController
    {
        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);
    }
}
