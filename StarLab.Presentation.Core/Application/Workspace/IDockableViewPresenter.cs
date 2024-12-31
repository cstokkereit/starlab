namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Defines the methods used by an <see cref="IDockableView"/> to communicate with its presenter.
    /// </summary>
    public interface IDockableViewPresenter : IPresenter
    {
        /// <summary>
        /// Gets or sets the current location of the view.
        /// </summary>
        string Location { get; set; }
    }
}
