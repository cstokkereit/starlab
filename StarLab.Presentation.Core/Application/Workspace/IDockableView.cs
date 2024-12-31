namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Defines the properties and methods used by an <see cref="IDockableViewPresenter"/> to control the behaviour of a tool window.
    /// </summary>
    public interface IDockableView : IView
    {
        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        void Initialise(IApplicationController controller);
    }
}
